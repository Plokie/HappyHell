using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : MonoBehaviour
{
    public TextMeshProUGUI epilepsy;
    public TextMeshProUGUI clickToContinue;

    public GameObject micCalibration;
    public TMP_Dropdown micChoice;
    public Slider slider;
    public TextMeshProUGUI header;
    public TextMeshProUGUI footer;

    IntroState state;

    string[] devices;
    string currentDevice;
    float micVol;
    int deviceSampleRate;
    AudioClip mainClip;

    float loudestVolume = 0f;
    float calTimer = 0f;

    enum IntroState
    {
        Epilepsy,
        MicChoice,
        MicCalibration,
        TitleScreen
    }

    // Start is called before the first frame update
    void Start()
    {
        micCalibration.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float calValue = 0f;

        switch (state)
        {
            case IntroState.Epilepsy:
                if(Input.GetMouseButtonDown(0))
                {
                    state = IntroState.MicChoice;
                    epilepsy.gameObject.SetActive(false);
                    clickToContinue.gameObject.SetActive(false);

                    micCalibration.gameObject.SetActive(true);

                    devices = Microphone.devices;

                    micChoice.AddOptions(new List<string>(devices));


                    int deviceSampleRateMin;
                    int deviceSampleRateMax;

                    Microphone.GetDeviceCaps(devices[0], out deviceSampleRateMin, out deviceSampleRateMax);
                    Debug.Log(devices[0] + "  " + deviceSampleRateMin.ToString() + "  " + deviceSampleRateMax.ToString()); // Print out info

                    deviceSampleRate = deviceSampleRateMin;

                    mainClip = Microphone.Start(devices[0], true, 999, deviceSampleRate);

                    currentDevice = devices[0];

                    while (!(Microphone.GetPosition(null) > 0)) { }
                }

                break;
            case IntroState.MicChoice:

                calValue = Calibrate(currentDevice);
                slider.value = calValue;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = IntroState.MicCalibration;
                    micChoice.gameObject.SetActive(false);
                    header.text = "Laugh to calibrate";
                    footer.text = String.Empty;
                }

                break;
            case IntroState.MicCalibration:

                calTimer += Time.deltaTime;

                calValue = Calibrate(currentDevice);
                slider.value = calValue;

                if (calValue > loudestVolume)
                {
                    loudestVolume = calValue;
                }

                if(calTimer > 3)
                {
                    state = IntroState.TitleScreen;
                }

                break;
            case IntroState.TitleScreen:



                break;
        }
    }

    float Calibrate(string device)
    {
        int samplesToProcess = 128;
        float[] data = new float[samplesToProcess];

        mainClip.GetData(data, Microphone.GetPosition(device) - (samplesToProcess + 1));

        float loudest = 0;

        for (int i = 0; i < samplesToProcess; i++)
        {
            if (data[i] > loudest)
            {
                loudest = data[i];
            }
        }

        return loudest;
    }

    public void ChangeDevice(Int32 value)
    {
        Microphone.End(currentDevice);

        currentDevice = devices[value];

        int deviceSampleRateMin;
        int deviceSampleRateMax;

        Microphone.GetDeviceCaps(devices[value], out deviceSampleRateMin, out deviceSampleRateMax);
        Debug.Log(devices[value] + "  " + deviceSampleRateMin.ToString() + "  " + deviceSampleRateMax.ToString()); // Print out info

        deviceSampleRate = deviceSampleRateMin;

        mainClip = Microphone.Start(currentDevice, true, 999, deviceSampleRate);

        while (!(Microphone.GetPosition(currentDevice) > 0)) { }
    }
}

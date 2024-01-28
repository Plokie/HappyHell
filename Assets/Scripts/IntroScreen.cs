using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Image micTest;

    IntroState state;

    string[] devices;
    string currentDevice;
    int deviceSampleRate;
    AudioClip mainClip;

    float loudestVolume = 0f;
    float calTimer = 0f;

    enum IntroState
    {
        Epilepsy,
        MicChoice,
        MicCalibration,
        MicTest,
        NoMic
    }

    // Start is called before the first frame update
    void Start()
    {
        micCalibration.gameObject.SetActive(false);
        micTest.gameObject.SetActive(false);
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

                    if(devices.Length == 0)
                    {
                        state = IntroState.NoMic;
                        break;
                    }

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

                

                calValue = Calibrate(currentDevice);
                slider.value = calValue;
                
                if (calValue > 0.1f)
                {
                    calTimer += Time.deltaTime;
                }

                if (calValue > loudestVolume)
                {
                    loudestVolume = calValue;
                }

                if(calTimer > 3)
                {
                    state = IntroState.MicTest;
                    micTest.gameObject.SetActive(true);
                    header.text = "Test mic\nIt should be green when laughing";

                    loudestVolume /= 2;
                    
                    clickToContinue.gameObject.SetActive(true);
                }

                break;
            case IntroState.MicTest:

                calValue = Calibrate(currentDevice);
                slider.value = calValue;

                if(calValue >= loudestVolume)
                {
                    micTest.color = Color.green;
                }
                else
                {
                    micTest.color = Color.white;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PlayerPrefs.SetFloat("LaughCutoff", loudestVolume);
                    PlayerPrefs.SetInt("DeviceIndex", micChoice.value);

                    SceneManager.LoadScene("Lara");
                }

                break;
            case IntroState.NoMic:

                header.text = "NO MICROPHONE DETECTED";

                footer.text = "Please plug a microphone in";

                if(Microphone.devices.Length > 0)
                {
                    SceneManager.LoadScene("Intro");
                }

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

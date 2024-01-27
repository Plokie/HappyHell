using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{

    public float laughCutoff = 0.1f;

    AudioClip mainClip;

    string deviceName;
    int deviceSampleRate;
    bool isSynced = false;

    // Start is called before the first frame update
    void Start()
    {
        int deviceSampleRateMin;
        int deviceSampleRateMax;

        deviceName = Microphone.devices[0]; // Using this for now, could try to let the player choose the microphone if we have time
        Microphone.GetDeviceCaps(deviceName, out deviceSampleRateMin, out deviceSampleRateMax);
        Debug.Log(deviceName + "  " + deviceSampleRateMin.ToString()  + "  " + deviceSampleRateMax.ToString()); // Print out info

        // Calc deviceSampleRate, ideally 44100 (CD Quality)

        if(deviceSampleRateMax >= 44100 && deviceSampleRateMin < 44100)
        {
            deviceSampleRate = 44100;
        }
        else
        {
            deviceSampleRate = deviceSampleRateMin;
        }

        Debug.Log(deviceSampleRate);

        mainClip = Microphone.Start(deviceName, true, 999, deviceSampleRate);

        while(!(Microphone.GetPosition(null) > 0)) { } // Wait for the mic to sync
        isSynced = true;
        
    }

    public bool CheckForLaugh()
    {
        if (isSynced)
        {
            bool laughed = false;
            int samplesToProcess = 128;
            float[] data = new float[samplesToProcess];

            mainClip.GetData(data, Microphone.GetPosition(deviceName) - (samplesToProcess + 1));

            for (int i = 0; i < samplesToProcess; i++)
            {
                if (data[i] > laughCutoff)
                {
                    laughed = true;
                }
            }

            return laughed;
        }
        else
        {
            return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
        PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeKeeper : MonoBehaviour
{
    public Sanity sanity;

    int numPeople;

    // Start is called before the first frame update
    void Start()
    {
        numPeople = GameObject.FindObjectsByType<NPC>(FindObjectsSortMode.None).Length;
        Debug.Log(numPeople);
    }

    // Update is called once per frame
    void Update()
    {
        if(sanity.HappyTasksComplete >= numPeople)
        {
            PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("WinState", 0);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOver");
        }

        if(sanity.Value <= 0)
        {
            PlayerPrefs.SetFloat("Time", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("WinState", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOver");
        }
    }
}

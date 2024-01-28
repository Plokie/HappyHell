using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winState;
    public TextMeshProUGUI winTime;


    // Start is called before the first frame update
    void Start()
    {
        winState.text = PlayerPrefs.GetInt("WinState") == 0 ? "You Win!!!" : "You Lose";
        winTime.text = PlayerPrefs.GetFloat("Time").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI winState;
    public TextMeshProUGUI winTime;


    // Start is called before the first frame update
    void Start()
    {
        winState.text = PlayerPrefs.GetInt("WinState") == 0 ? "You Win!!!" : "You Lose";
        winTime.text = "Time: " + PlayerPrefs.GetFloat("Time").ToString();

        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Lara");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}

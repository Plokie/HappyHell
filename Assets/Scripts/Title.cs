using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject cam;
    public TextMeshProUGUI logo;
    float rotValue = 0f;

    float flicker = 0;
    float flickerTimer = 0;

    public float speed = 3f;

    string lightStyle = "mmamammmmammamamaaamammmam"; // From quake
    int lightPos = 0;

    // Update is called once per frame
    void Update()
    {
        rotValue += Time.deltaTime;
        flicker += Time.deltaTime;
        flickerTimer += Time.deltaTime;

        if (flickerTimer > 5f)
        {
            if (flicker >= 0.1f) // 10fps
            {
                char action = lightStyle[lightPos];

                switch (action)
                {
                    case 'm':

                        logo.text = "Happy Hell<color=white>o</color>";

                        break;
                    case 'a':

                        logo.text = "Happy Hell<color=#D9D9D9>o</color>";

                        break;
                }

                lightPos++;

                if (lightPos >= lightStyle.Length)
                {
                    lightPos = 0;
                    flickerTimer = 0;
                }

                flicker = 0;
            }
        }

        cam.transform.Rotate(0, Mathf.Sin(rotValue) * speed, 0);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Lara");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

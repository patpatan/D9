using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Pauseadvertisement : MonoBehaviour
{
    public bool gamePause = false;
    public GUISkin GamexSkin;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Advertisement.Initialize("3919035");
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePause == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    void OnGUI()
    {

        GUI.skin = GamexSkin;
        if (GUI.Button(new Rect(Screen.width - 150, 20, 110, 55),
                       "Pause"))
        {
            CallAds();
        }

        if (gamePause)
        {
            Time.timeScale = 0;
            if (GUI.Button(new Rect(Screen.width / 4 + 10,
                                    Screen.height / 4 + Screen.height / 10 + 60,
                                    Screen.width / 2 - 20, Screen.height / 10),
                           "Return"))
            {
                gamePause = false;
            }


        }
        else
        {
            Time.timeScale = 1;
        }
    }
    void CallAds()
    {

        print("Woohoo");

        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            gamePause = true;
        }

    }
}

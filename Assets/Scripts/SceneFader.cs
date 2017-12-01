using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    //public float _fadeDuration = 2f;

    private void Start()
    {
        FadeToBlack(0.2f);
    }

    public static void FadeToBlack(float fadeTime)
    {
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.black, fadeTime);
    }
    public static void FadeFromBlack(float fadeTime)
    {
        //set start color
        SteamVR_Fade.Start(Color.black, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear, fadeTime);
    }
}

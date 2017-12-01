using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadTrack : MonoBehaviour {

    public GameObject monster;
    private float delay = 0.5f;
    private bool sceneStarted = false;
 
    //private bool playedClip = false;
    //public GameObject child;

    private void Update()
    {
        if (delay <= 0 && !sceneStarted)
        {
            SceneFader.FadeFromBlack(2f);
            sceneStarted = true;
        }
        else if (delay > 0 && !sceneStarted)
        {
            delay -= Time.deltaTime;
        }
        if (Flashlight.gameOver)
        {
            StartCoroutine("WaitAndLoad");
            SceneFader.FadeToBlack(2f);
        }
    }

    private IEnumerator WaitAndLoad()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.1f);
            SceneLoader();
        }
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene(3);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (Controller.GetHairTriggerDown())
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
        SceneManager.LoadScene(1);
    }
}

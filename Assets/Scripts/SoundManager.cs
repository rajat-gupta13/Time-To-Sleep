using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject guest, guestCamera;
    public GameObject child;
    private int clipCount = 0;
    public AudioClip[] hallwayClips;
    private bool fadeFromBlack = true;
    // Use this for initialization

    void Start () {
        child.GetComponent<AudioSource>().clip = hallwayClips[0];
        child.GetComponent<AudioSource>().Play();
        clipCount = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (!child.GetComponent<AudioSource>().isPlaying && !guestCamera.GetComponent<AudioSource>().isPlaying && clipCount == 1)
        {
            guestCamera.GetComponent<AudioSource>().clip = hallwayClips[1];
            guestCamera.GetComponent<AudioSource>().Play();
            clipCount = 2;
        }
        if (!child.GetComponent<AudioSource>().isPlaying && !guestCamera.GetComponent<AudioSource>().isPlaying && clipCount == 2)
        {
            guestCamera.GetComponent<AudioSource>().clip = hallwayClips[2];
            guestCamera.GetComponent<AudioSource>().Play();
            clipCount = 3;
        }
        if (!child.GetComponent<AudioSource>().isPlaying && !guestCamera.GetComponent<AudioSource>().isPlaying && clipCount == 3)
        {
            if (fadeFromBlack)
            {
                SceneLoader();
            }
            //guest.GetComponent<AudioSource>().clip = hallwayClips[3];
            //guest.GetComponent<AudioSource>().Play();
            //clipCount = 4;
        }
    }

    void SceneLoader()
    {
        SceneFader.FadeFromBlack(2f);
        fadeFromBlack = false;
    }
}

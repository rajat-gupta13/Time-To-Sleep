using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHallwayDrawer : MonoBehaviour {

    public GameObject guest, flashlight;
    public AudioClip grabClip;

    public void HallwayDrawerOpen()
    {
        GetComponent<Animator>().SetBool("HallwayDrawerOpen", false);
        GetComponent<Animator>().SetBool("HallwayDrawerClose", false);
        if (!HallwayGrab.leftHasFlashlight && !HallwayGrab.rightHasFlashlight && HallwayGrab.openedOnce)
        {
            flashlight.SetActive(true);
            guest.GetComponent<AudioSource>().clip = grabClip;
            guest.GetComponent<AudioSource>().Play();
        }
    }
    public void HallwayDrawerClose()
    {
        GetComponent<Animator>().SetBool("HallwayDrawerClose", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    public GameObject leftController, rightController, leftFlashlightModel, rightFlashlightModel, leftControllerModel, rightControllerModel;

    void Awake()
    {
        if (HallwayGrab.leftHasFlashlight)
        {
            leftController.GetComponent<Credits>().enabled = false;
            leftFlashlightModel.SetActive(true);
            leftControllerModel.SetActive(false);
        }
        else if (HallwayGrab.rightHasFlashlight)
        {
            rightController.GetComponent<Credits>().enabled = false;
            rightFlashlightModel.SetActive(true);
            rightControllerModel.SetActive(false);
        }
    }
}

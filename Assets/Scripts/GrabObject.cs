using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    public static bool topDrawerOpen = false;
    public static bool chestOpen = false;
    private bool windowOpen = false;
    public static  bool closetOpen = false;
    public GameObject topDrawer, leftControllerModel, leftFlashlightModel, rightControllerModel, rightFlashlightModel,
        drawerFinal, toyChest, toyChestFinal, window, windowFinal, monster, chestCollider,
        closetDoorLeft, closetDoorRight, closetDoorFinal, leftController, rightController;
    public AudioClip doorOpen, doorClose, drawerOpen, drawerClose, sqeek;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        if (HallwayGrab.leftHasFlashlight)
        {
            leftController.GetComponent<GrabObject>().enabled = false;
            leftFlashlightModel.SetActive(true);
            leftControllerModel.SetActive(false);
        }
        else if (HallwayGrab.rightHasFlashlight)
        {
            rightController.GetComponent<GrabObject>().enabled = false;
            rightFlashlightModel.SetActive(true);
            rightControllerModel.SetActive(false);
        }
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log("Colliding with " + collidingObject.name + " topDrawerOpen= " + topDrawerOpen + " chestOpen= " + chestOpen + " windowOpen= " + windowOpen + " closetOpen= " + closetOpen);
            if (collidingObject == topDrawer && !topDrawerOpen)
            {
                drawerFinal.GetComponent<Animator>().SetBool("CupboardOpen", true);
                topDrawer.GetComponent<AudioSource>().clip = doorOpen;
                topDrawer.GetComponent<AudioSource>().Play();
                //drawerCollider.GetComponent<BoxCollider>().enabled = false;
                topDrawerOpen = true;
                monster.GetComponent<AudioSource>().Stop();
            }
            if (collidingObject == topDrawer && topDrawerOpen)
            {
                drawerFinal.GetComponent<Animator>().SetBool("CupboardClose", true);
                topDrawer.GetComponent<AudioSource>().clip = doorClose;
                topDrawer.GetComponent<AudioSource>().Play();
                //drawerCollider.GetComponent<BoxCollider>().enabled = true;
                topDrawerOpen = false;
            }
            if (collidingObject == toyChest && !chestOpen)
            {
                toyChestFinal.GetComponent<Animator>().SetBool("ChestOpen", true);
                toyChestFinal.GetComponent<AudioSource>().clip = doorOpen;
                toyChestFinal.GetComponent<AudioSource>().Play();
                //toyChestFinal.GetComponent<BoxCollider>().enabled = false;
                chestOpen = true;
                monster.GetComponent<AudioSource>().Stop();
                chestCollider.GetComponent<BoxCollider>().enabled = false;
            }
            if (collidingObject == toyChest && chestOpen)
            {
                toyChestFinal.GetComponent<Animator>().SetBool("ChestClose", true);
                toyChestFinal.GetComponent<AudioSource>().clip = doorClose;
                toyChestFinal.GetComponent<AudioSource>().Play();
                //toyChestFinal.GetComponent<BoxCollider>().enabled = true;
                chestOpen = false;
                chestCollider.GetComponent<BoxCollider>().enabled = true;
            }
            if (collidingObject == window && !windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowOpen", true);
                windowFinal.GetComponent<AudioSource>().clip = drawerOpen;
                windowFinal.GetComponent<AudioSource>().Play();
                windowOpen = true;
            }
            if (collidingObject == window && windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowClose", true);
                windowFinal.GetComponent<AudioSource>().clip = drawerClose;
                windowFinal.GetComponent<AudioSource>().Play();
                windowOpen = false;
            }
            if ((collidingObject == closetDoorLeft || collidingObject == closetDoorRight) && !closetOpen)
            {
                closetDoorFinal.GetComponent<Animator>().SetBool("ClosetOpen", true);
                closetDoorFinal.GetComponent<AudioSource>().clip = doorOpen;
                closetDoorFinal.GetComponent<AudioSource>().Play();
                closetOpen = true;
                monster.GetComponent<AudioSource>().Stop();
            }
            if ((collidingObject == closetDoorLeft || collidingObject == closetDoorRight) && closetOpen)
            {
                closetDoorFinal.GetComponent<Animator>().SetBool("ClosetClose", true);
                closetDoorFinal.GetComponent<AudioSource>().clip = doorClose;
                closetDoorFinal.GetComponent<AudioSource>().Play();
                closetOpen = false;
            }
          

        }

        if (Controller.GetHairTriggerUp())
        {
            if (collidingObject == topDrawer)
            {
                drawerFinal.GetComponent<Animator>().SetBool("CupboardOpen", false);
            }
            if (collidingObject == topDrawer && topDrawerOpen)
            {
                drawerFinal.GetComponent<Animator>().SetBool("CupboardClose", false);
            }
            if (collidingObject == toyChest && !chestOpen)
            {
                toyChestFinal.GetComponent<Animator>().SetBool("ChestOpen", false);
            }
            if (collidingObject == toyChest && chestOpen)
            {
                toyChestFinal.GetComponent<Animator>().SetBool("ChestClose", false);
            }
            if (collidingObject == window && !windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowOpen", false);
            }
            if (collidingObject == window && windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowClose", false);
            }
            if ((collidingObject == closetDoorLeft || collidingObject == closetDoorRight) && !closetOpen)
            {
                closetDoorFinal.GetComponent<Animator>().SetBool("ClosetOpen", false);
            }
            if ((collidingObject == closetDoorLeft || collidingObject == closetDoorRight) && closetOpen)
            {
                closetDoorFinal.GetComponent<Animator>().SetBool("ClosetClose", false);
            }

            
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void Grab()
    {
       
        objectInHand = collidingObject;
        collidingObject = null;
       
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
    
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }
}

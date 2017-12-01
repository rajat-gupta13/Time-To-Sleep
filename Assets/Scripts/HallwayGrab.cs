using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayGrab : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    public AudioClip doorOpen, drawerOpenClip, drawerClose, flashlightClip;
    public GameObject door, doorFinal, flashlight, flashlighModel, controllerModel, hallwayDrawer, hallwayDrawerFinal, window, windowFinal;
    public static bool leftHasFlashlight = false;
    public static bool rightHasFlashlight = false;
    private bool drawerOpen = false;
    private bool windowOpen = false;
    public GameObject leftController, rightController, guest;
    public static bool openedOnce = false;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
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

    private void SceneLoader()
    {
        SceneManager.LoadScene(2);
    }

    private void FixedUpdate ()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject == hallwayDrawer && !drawerOpen)
            {
                hallwayDrawerFinal.GetComponent<Animator>().SetBool("HallwayDrawerOpen", true);
                hallwayDrawerFinal.GetComponent<AudioSource>().clip = drawerOpenClip;
                hallwayDrawerFinal.GetComponent<AudioSource>().Play();
                drawerOpen = true;
                openedOnce = true;
            }

            if (collidingObject == hallwayDrawer && drawerOpen)
            {
                flashlight.SetActive(false);
                hallwayDrawerFinal.GetComponent<Animator>().SetBool("HallwayDrawerClose", true);
                hallwayDrawerFinal.GetComponent<AudioSource>().clip = drawerClose;
                hallwayDrawerFinal.GetComponent<AudioSource>().Play();
                drawerOpen = false;
            }

            if (collidingObject == door)
            {
                doorFinal.GetComponent<Animator>().SetBool("OpenDoor", true);
                doorFinal.GetComponent<AudioSource>().clip = doorOpen;
                doorFinal.GetComponent<AudioSource>().Play();
                if (leftHasFlashlight || rightHasFlashlight)
                {
                    StartCoroutine("WaitAndLoad");
                    SceneFader.FadeToBlack(2f);
                }
            }

            if (collidingObject == window && !windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowOpen", true);
                windowFinal.GetComponent<AudioSource>().clip = drawerOpenClip;
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

            if (collidingObject == flashlight)
            {
                Grab();
                flashlight.SetActive(false);
                flashlighModel.SetActive(true);
                controllerModel.SetActive(false);
                hallwayDrawerFinal.GetComponent<AudioSource>().clip = flashlightClip;
                hallwayDrawerFinal.GetComponent<AudioSource>().Play();
                flashlighModel.transform.parent.GetComponent<HallwayGrab>().enabled = false;
                if (flashlighModel.transform.parent.gameObject == leftController)
                {
                    leftHasFlashlight = true;
                    rightHasFlashlight = false;
                }
                else if (flashlighModel.transform.parent.gameObject == rightController)
                {
                    rightHasFlashlight = true;
                    leftHasFlashlight = false;
                }
                Debug.Log("Left= " + leftHasFlashlight + " , Right= " + rightHasFlashlight);
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (collidingObject == door)
            {
                doorFinal.GetComponent<Animator>().SetBool("OpenDoor",false);
            }
            if (collidingObject == hallwayDrawer && !drawerOpen)
            {
                hallwayDrawerFinal.GetComponent<Animator>().SetBool("HallwayDrawerOpen", false);
            }
            if (collidingObject == hallwayDrawer && drawerOpen)
            {
                hallwayDrawerFinal.GetComponent<Animator>().SetBool("HallwayDrawerClose", false);
            }
            if (collidingObject == window && !windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowOpen", false);
            }
            if (collidingObject == window && windowOpen)
            {
                windowFinal.GetComponent<Animator>().SetBool("WindowClose", false);
            }
        }
    }

    private IEnumerator WaitAndLoad()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            SceneLoader();
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
    

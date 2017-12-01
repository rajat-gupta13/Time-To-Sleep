using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

	
    public void OpenDoor()
    {
        GetComponent<Animator>().SetBool("OpenDoor",false);
    } 
}

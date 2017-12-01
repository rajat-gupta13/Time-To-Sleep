using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpen : MonoBehaviour {

    public void OpenWindow()
    {
        GetComponent<Animator>().SetBool("WindowOpen", false);
        GetComponent<Animator>().SetBool("WindowClose", false);
    }

    public void CloseWindow()
    {
        GetComponent<Animator>().SetBool("WindowClose", false);
        GetComponent<Animator>().SetBool("WindowOpen", false);
    }
}

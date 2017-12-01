using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOpen : MonoBehaviour {

    public GameObject monster;
    public static bool monsterMovable = false;
    public void OpenCupboard()
    {
        GetComponent<Animator>().SetBool("CupboardOpen", false);
        GetComponent<Animator>().SetBool("CupboardClose", false);
        //GetComponent<BoxCollider>().enabled = false;
    }
    public void CloseCupboard()
    {
        GetComponent<Animator>().SetBool("CupboardClose", false);
    }

    public void MoveMonster()
    {
        monster.GetComponent<AudioSource>().Stop();
        monsterMovable = true;
    }

    public void StopMonster()
    {
        monsterMovable = false;
    }
}

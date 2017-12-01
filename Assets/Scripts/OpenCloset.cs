using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloset : MonoBehaviour {

    public GameObject monster;
    public static bool monsterMovable = false;

    public void ClosetOpen()
    {
        GetComponent<Animator>().SetBool("ClosetOpen", false);
        GetComponent<Animator>().SetBool("ClosetClose", false);
    }

    public void ClosetClose()
    {
        GetComponent<Animator>().SetBool("ClosetClose", false);
        GetComponent<Animator>().SetBool("ClosetOpen", false);
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

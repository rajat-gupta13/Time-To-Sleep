using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyChest : MonoBehaviour {

    public GameObject monster;
    public static bool monsterMovable = false;
	
    public void ChestOpen()
    {
        GetComponent<Animator>().SetBool("ChestOpen", false);
        GetComponent<Animator>().SetBool("ChestClose", false);
    }

    public void ChestClose()
    {
        GetComponent<Animator>().SetBool("ChestClose", false);
        GetComponent<Animator>().SetBool("ChestOpen", false);
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

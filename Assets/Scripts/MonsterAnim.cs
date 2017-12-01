using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour {

    public Transform player;

    public void IdleAnim()
    {
        GetComponent<Animator>().SetBool("MonsterSurprised", false);
        GetComponent<Animator>().SetBool("MonsterNotSurprised", false);
    }
    public void SurpriseAnim()
    {
        GetComponent<Animator>().SetBool("MonsterSurprised", false);
        GetComponent<Animator>().SetBool("MonsterNotSurprised", false);
    }

    private void Update()
    {
        transform.LookAt(player);
    }
}

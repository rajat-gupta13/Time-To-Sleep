using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHallway : MonoBehaviour {
    public GameObject soundManager;
    public GameObject guest;
    public GameObject child;
    public AudioClip hallwayClip2;
    public static bool hallwayClipPlayed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == soundManager)
        {
            child.GetComponent<AudioSource>().clip = hallwayClip2;
            child.GetComponent<AudioSource>().Play();
            hallwayClipPlayed = true;
        }
    }
}

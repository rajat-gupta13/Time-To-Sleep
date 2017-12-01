using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flashlight : MonoBehaviour {

    public GameObject monster;
    public Transform[] hidingPlaces;
    [Range(0,0.001f)]public float scalingFactor;
    public float callWaitTime = 3.0f;
    //private bool flashlightPointed = false;
    private GameObject currentPosition, targetPosition;
    public Camera headCamera;
    private int index = 0;
    private bool monsterMoving = false;
    public AudioClip monsterHissing, monsterScratching, child1, child2, child3, bgm;
    public float speed;
    public Transform[] target;
    int randomNumber = 0;
    List<int> usedValues = new List<int>();
    private bool executedOnce = false;
    private int mask = 127;
    public GameObject explosion;
    private bool enemyDestroyed = false;
    public GameObject child;
    private bool played2 = false;
    private bool played3 = false;
    public static bool gameOver = false;
    public GameObject boy, cupboard, closet, chest, soundManager;

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        return val;
    }

    private void Start()
    {
        child.GetComponent<AudioSource>().clip = child1;
        child.GetComponent<AudioSource> ().Play();
    }


    private void Update()
    {
        if (monster.transform.position == hidingPlaces[0].transform.position && !child.GetComponent<AudioSource>().isPlaying && !played2)
        {
            child.GetComponent<AudioSource>().clip = child2;
            child.GetComponent<AudioSource>().Play();
            played2 = true;
        }

        if (monster.transform.position == hidingPlaces[1].transform.position && !DrawerOpen.monsterMovable && !monster.GetComponent<AudioSource>().isPlaying)
        {
            monster.GetComponent<AudioSource>().clip = monsterScratching;
            monster.GetComponent<AudioSource>().Play();
        }
        else if (monster.transform.position == hidingPlaces[2].transform.position && !ToyChest.monsterMovable && !monster.GetComponent<AudioSource>().isPlaying)
        {
            monster.GetComponent<AudioSource>().clip = monsterScratching;
            monster.GetComponent<AudioSource>().Play();
        }
        else if (monster.transform.position == hidingPlaces[3].transform.position && !OpenCloset.monsterMovable && !monster.GetComponent<AudioSource>().isPlaying)
        {
            monster.GetComponent<AudioSource>().clip = monsterScratching;
            monster.GetComponent<AudioSource>().Play();
        }
        else if (monster.transform.position == hidingPlaces[4].transform.position && !ToyChest.monsterMovable && !monster.GetComponent<AudioSource>().isPlaying)
        {
            monster.GetComponent<AudioSource>().clip = monsterScratching;
            monster.GetComponent<AudioSource>().Play();
        }
        else if (monster.transform.position == hidingPlaces[5].transform.position && !DrawerOpen.monsterMovable && !monster.GetComponent<AudioSource>().isPlaying)
        {
            monster.GetComponent<AudioSource>().clip = monsterScratching;
            monster.GetComponent<AudioSource>().Play();
        }
     
        if (monster.transform.position == hidingPlaces[3].transform.position && OpenCloset.monsterMovable)
        {
                Debug.Log("Closet Open");
                monsterMoving = true;
                StartCoroutine(MoveMonster());
            soundManager.GetComponent<AudioSource>().clip = bgm;
            soundManager.GetComponent<AudioSource>().Play();
        }
        if (monster.transform.position == hidingPlaces[4].transform.position && ToyChest.monsterMovable)
        {
                monsterMoving = true;
                StartCoroutine(MoveMonster());
        }
        if (monster.transform.position == hidingPlaces[5].transform.position && DrawerOpen.monsterMovable)
        {
                monsterMoving = true;
                StartCoroutine(MoveMonster());
        }

        if (played2 && !child.GetComponent<AudioSource>().isPlaying)
        {
            boy.GetComponent<BoxCollider>().enabled = false;
        }

        if (enemyDestroyed && !child.GetComponent<AudioSource>().isPlaying && !played3)
        {
            child.GetComponent<AudioSource>().clip = child3;
            child.GetComponent<AudioSource>().Play();
            played3 = true;
        }
        if (enemyDestroyed && !child.GetComponent<AudioSource>().isPlaying && played3)
        {
            gameOver = true;
        }
    }

   

    void ChangeHidingPlace()
    {
        if (index < 6)
        {
            Debug.Log(index);
            monster.transform.position = hidingPlaces[index].position;
            index++;
            monsterMoving = false;
            executedOnce = false;
            cupboard.GetComponent<Animator>().SetBool("CupboardClose", true);
            closet.GetComponent<Animator>().SetBool("ClosetClose", true);
            chest.GetComponent<Animator>().SetBool("ChestClose", true);
            //monster.GetComponent<AudioSource>().clip = monsterScratching;
            //monster.GetComponent<AudioSource>().Play();
        }
        else if (index == 6)
        {
            Instantiate(explosion, monster.transform.position, monster.transform.rotation);
            monster.SetActive(false);
            enemyDestroyed = true;
            soundManager.GetComponent<AudioSource>().Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent == monster.transform)
        {
            Debug.Log("In trigger");
            RaycastHit hit;
            if (Physics.Raycast(headCamera.transform.position, (monster.transform.position - headCamera.transform.position).normalized, out hit, Mathf.Infinity, mask ))
            {
                //Debug.DrawRay(transform.position, (monster.transform.position - headCamera.transform.position).normalized * 5, Color.yellow, 10f);
                Debug.Log("hit: " + hit.transform.gameObject.name);
                if (hit.transform.parent == monster.transform) {
                    if (!executedOnce)
                    {
                        Invoke("ChangeHidingPlace", monsterHissing.length);
                        Debug.Log("Called funct");
                        //monster.GetComponent<AudioSource>().Stop();
                        monster.GetComponent<AudioSource>().clip = monsterHissing;
                        monster.GetComponent<AudioSource>().Play();
                        monster.GetComponent<Animator>().SetBool("MonsterSurprised", true);
                        executedOnce = true;
                    }
                    Debug.Log("Flashlight Pointed at Monster");
                    //flashlightPointed = true;
                    monster.transform.localScale -= new Vector3(scalingFactor, scalingFactor, scalingFactor);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent == monster.transform)
        {
            monster.GetComponent<Animator>().SetBool("MonsterNotSurprised", true);
            //monster.GetComponent<AudioSource>().Stop();
            //flashlightPointed = false;
            
        }
    }

    private IEnumerator MoveMonster()
    {
        randomNumber = 0;
        Debug.Log("In monster function");
        while (monsterMoving)
        {
            Vector3 diff = new Vector3(0.05f, 0.05f, 0.05f);
            Debug.Log("Monster Moving");
            monster.transform.Translate((target[randomNumber].position - monster.transform.position).normalized * Time.deltaTime * speed, Space.World );
            if ((monster.transform.position.x - target[randomNumber].transform.position.x) <= diff.x && (monster.transform.position.y - target[randomNumber].transform.position.y) <= diff.y && (monster.transform.position.z - target[randomNumber].transform.position.z) <= diff.z)
            {
                Debug.Log("Changing target");
                randomNumber++;
                if (randomNumber == target.Length)
                {
                    randomNumber = 0;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}

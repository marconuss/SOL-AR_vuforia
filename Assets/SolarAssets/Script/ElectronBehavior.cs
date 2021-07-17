using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElectronBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    float Ypos;
    float Xpos;
    Vector3 originalPos;

    float frequency = 2f;
    float amplitude = 1f;
    public float moveSpeed = 5f;

    bool moveToLeft = false;

    bool respawned = false;

    Animator animator;

    bool moving = false;

    void Start()
    {
        originalPos = transform.position;
        Xpos = originalPos.x;


        animator = GetComponentInParent<Animator>();
        if (GameManager.instance.circuitActive == false) StartCoroutine("DislodgeElectron");
        if (GameManager.instance.circuitActive == true && respawned == false)
        {
            StartCoroutine("MoveElectronToGrid");       
        }
    }

    void Update()
    {
        if(moving) CircuitMovement();
    }

    void CircuitMovement()
    {
            Ypos = originalPos.y;
            Ypos += Mathf.Sin((Time.time * Mathf.PI * frequency)) * amplitude;

            if (moveToLeft == false) Xpos += moveSpeed * Time.deltaTime;

            if (moveToLeft == true)
            {
                Xpos -= moveSpeed * Time.deltaTime;

                if (Xpos <= 25)
                {
                    StartCoroutine("MoveElectronToGrid");
                }
            }


        transform.position = new Vector3(Xpos, Ypos, 0);
    }
    


    IEnumerator DislodgeElectron()
    {
        animator.Play("dislodge");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
        
    }

    IEnumerator MoveElectronToGrid()
    {
        if (respawned == false)
        {
            animator.Play("moveToGrid");
            yield return new WaitForSeconds(1.1f);
        }
        else if (respawned == true)
        {
            moving = false;
            animator.Play("moveToGrid");
            yield return new WaitForSeconds(1.1f);
            Destroy(gameObject);
        }

        moving = true;
        originalPos = transform.position;
        Xpos = originalPos.x;
    }

    void SpawnSecondRowElectrons(GameObject collidedWire)
    {
        Vector3 spawnPos;

        GameObject[] wireObjects = GameObject.FindGameObjectsWithTag("WireConnector");

        for (int i = 0; i < wireObjects.Length; i++)
        {
            if (wireObjects[i] != collidedWire)
            {
                spawnPos = wireObjects[i].transform.position;
                GameObject newElectron = Instantiate(GameManager.instance.electronPrefab, spawnPos, Quaternion.identity, GameManager.instance.electronParent.transform);
                newElectron.GetComponent<ElectronBehavior>().moveToLeft = true;
                newElectron.GetComponent<ElectronBehavior>().respawned = true;
                newElectron.GetComponent<ElectronBehavior>().moving = true;
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "WireConnector" && respawned == false)
        {

            SpawnSecondRowElectrons(collision.gameObject);
            Destroy(gameObject);
            //play animation

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sirKit : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public bool litUpNot = true;



    private int wIndex;
    private bool arrived;
    private bool startSecondPhase;
    private Animator animator;
    private Vector2 startPos;
    void Start()
    {
        animator = GetComponent<Animator>();    
        wIndex = 0;
        arrived = false;

        startPos = transform.position;
    }

    //public void TestCircuitAnimationPlay()
    private void Update()
    {
        //if(startSecondPhase)
            MoveToCircuitPos();
    }

    public void MoveToCircuitPos()
    {
        float step = speed * Time.deltaTime;

        if(wIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[wIndex].position, step);
            if(Vector2.Distance(transform.position, waypoints[wIndex].position) < 0.005f)
            {
                if(wIndex == waypoints.Length-1)
                {
                    arrived = true;
                }
                else{
                    wIndex++;
                }
            }
        }
        if(arrived && (!animator.GetCurrentAnimatorStateInfo(0).IsName("lowVolt") || 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("highVolt")))
            {
                animator.Play("lowVolt", 0);
                if(!litUpNot)
                    animator.SetBool("circuitComplete", true);
            }


    }
}

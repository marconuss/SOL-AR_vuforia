using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sirKit : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public bool litUp;


    private int wIndex;
    private bool arrived;
    private bool activateCircuitAnim;
    private Animator animator;
    private Vector3 startPos;


    void Start()
    {
        animator = GetComponent<Animator>();    
        wIndex = 0;
        arrived = false;
        litUp = false;
        activateCircuitAnim = false;
        startPos = transform.position;
    }

    //public void TestCircuitAnimationPlay()
    private void Update()
    {
        activateCircuitAnim = GameManager.instance.fungusManager.GetBooleanVariable("circuitAnimBool");
        if(activateCircuitAnim)
            MoveToCircuitPos();
        else
            MoveBackToPos();
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
        if(arrived && (!animator.GetCurrentAnimatorStateInfo(0).IsName("lowVolt") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("highVolt")))
            {
                animator.Play("lowVolt", 0);

            }
        //if(litUp)
        //{
        //    animator.SetBool("circuitComplete", true);
        //}
    }
    public void MoveBackToPos()
    {
        animator.Play("idle", 0);
        float step = speed * Time.deltaTime;

        if(transform.position != startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, step);
            if(Vector2.Distance(transform.position, waypoints[wIndex].position) < 0.005f)
            {
                transform.position = startPos;
            }
        }
        if(GameManager.instance.secondPhase)
        {
            GameManager.instance.fungusManager.SetBooleanVariable("insidePhase2", true);
        }
    }
}

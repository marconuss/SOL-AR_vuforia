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
    public bool activateCircuitAnim;

    private Animator childAnimator;
    private Animator parentAnimator;
    private Vector3 startPos;
    private bool backToStart = true;


    void Start()
    {
        childAnimator = transform.GetChild(0).GetComponent<Animator>();    
        parentAnimator = GetComponent<Animator>();    
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

        parentAnimator.SetBool("move", true);

        //parentAnimator.applyRootMotion = true;
        //parentAnimator.speed = 0;
        //
        //float step = speed * Time.deltaTime;
        //
        //if(wIndex < waypoints.Length)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, waypoints[wIndex].position, step);
        //    if(Vector2.Distance(transform.position, waypoints[wIndex].position) < 0.005f)
        //    {
        //        if(wIndex == waypoints.Length-1)
        //        {
        //            arrived = true;
        //        }
        //        else{
        //            wIndex++;
        //        }
        //    }
        //}

        if((parentAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            && parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("moveToCircuit")) 
        {
            arrived = true;
            backToStart = false;
        }

        if(arrived && (!childAnimator.GetCurrentAnimatorStateInfo(0).IsName("connection") && 
            !childAnimator.GetCurrentAnimatorStateInfo(0).IsName("highVolt")))
        {
            childAnimator.Play("connection", 0);
            
        }
        else if(!arrived)
        {
            childAnimator.Play("idle", 0);
        }
            
        //if(litUp)
        //{
        //    childAnimator.SetBool("circuitComplete", true);
        //}
    }
    public void MoveBackToPos()
    {

        parentAnimator.SetBool("move", false);

        if (!backToStart)
        {
            childAnimator.Play("idle", 0);
            backToStart = true;
            arrived = false;
            if(GameManager.instance.fungusManager.GetBooleanVariable("insidePhase2") == true)
            {
                GameManager.instance.ActivateSecondPhase();
            }
        }

        //float step = speed * Time.deltaTime;
        //
        //arrived = false;
        //
        //if(transform.position != startPos)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, startPos, step);
        //    if(Vector2.Distance(transform.position, waypoints[wIndex].position) < 0.005f)
        //    {
        //        Debug.Log("arrived");
        //        parentAnimator.applyRootMotion = false;
        //    }
        //}
        //if(GameManager.instance.secondPhase)
        //{
        //    GameManager.instance.fungusManager.SetBooleanVariable("insidePhase2", true);
        //}
    }
}

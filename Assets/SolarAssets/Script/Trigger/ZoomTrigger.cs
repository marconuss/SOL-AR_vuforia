using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{

    private GameObject zoomObject;

    private Collider2D cardCollider;
    private bool onTriggerFirst;


    void Start()
    {
        zoomObject = GameObject.FindGameObjectWithTag("ZoomImage");
    }


    private void Update()
    {
        if (onTriggerFirst && cardCollider)
        {
            if (!cardCollider.gameObject.GetComponent<CardPickup>().mouseHold)
                PlaceCardOnTrigger(cardCollider);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cardCollider = collision;
        onTriggerFirst = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.fungusManager.ExecuteBlock("ZoomOutHelper");
        onTriggerFirst = false;
    }

    private void PlaceCardOnTrigger(Collider2D collision)
    {
        if (!GameManager.instance.secondPhase)
        {
            int cardIndex = (int)collision.gameObject.GetComponent<Card>().type;

            GameManager.instance.fungusManager.SetIntegerVariable("cardOnZoom", cardIndex);

            GameManager.instance.fungusManager.ExecuteBlock("ZoomInHelper");
        }
        onTriggerFirst = false;
    }
}

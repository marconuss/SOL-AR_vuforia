using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CardsOnTrigger;

    [SerializeField]
    bool cardPlaced = false;

    private Collider2D cardCollider;
    private bool onTriggerFirst;


    private void Update()
    {
        if(onTriggerFirst && cardCollider)
        {
            if(!cardCollider.gameObject.GetComponent<CardPickup>().mouseHold)
                PlaceCardOnTrigger(cardCollider);
        }
    }

    public List<GameObject> GetCardsOnTrigger()
    {
        return CardsOnTrigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Card placed");

        if (other.gameObject.tag == "Cards")
        {
            if(!cardPlaced) cardPlaced = true;
            //Debug.Log(other.gameObject.name + " on " + this.gameObject.name);
            CardsOnTrigger.Add(other.gameObject);
            CardManager.instance.PlaceCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, other.gameObject.GetComponent<Card>());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cardCollider = collision;
        onTriggerFirst = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         Vector2 positionOnTrigger = TriggerGrid.instance.GetTriggerGridPosition(this.gameObject);
        if (collision.gameObject.tag == "Cards")
        {
            if (this.gameObject.tag != "ZoomTrigger")
            {
                CardsOnTrigger.Remove(collision.gameObject);
                CardManager.instance.RemoveCard((int)positionOnTrigger.x, (int)positionOnTrigger.y, collision.gameObject.GetComponent<Card>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cards")
        {
            if (other.gameObject.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                cardPlaced = true;
                return;
            }

            if (cardPlaced == true)
            {
                cardPlaced = false;
                CardsOnTrigger.Remove(other.gameObject);
                CardManager.instance.RemoveCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, other.gameObject.GetComponent<Card>());

            }
        }
        onTriggerFirst = false;
    }

    private void PlaceCardOnTrigger(Collider2D collision)
    {
        Vector2 positionOnTrigger = TriggerGrid.instance.GetTriggerGridPosition(this.gameObject);
        if (collision.gameObject.tag == "Cards")
        {
            //Debug.Log("Card placed");
            if (this.gameObject.tag != "ZoomTrigger")
            {
                if ((GameManager.instance.secondPhase == false) && (int)positionOnTrigger.x == 0)
                {
                    Debug.Log(collision.gameObject.name + " on " + this.gameObject.name);

                    CardsOnTrigger.Add(collision.gameObject);
                    CardManager.instance.PlaceCard((int)positionOnTrigger.x, (int)positionOnTrigger.y, collision.gameObject.GetComponent<Card>());
                }
                else if ((GameManager.instance.secondPhase == true) && (int)positionOnTrigger.x == 1)
                {
                    Debug.Log(collision.gameObject.name + " on " + this.gameObject.name);

                    CardsOnTrigger.Add(collision.gameObject);
                    CardManager.instance.PlaceCard((int)positionOnTrigger.x, (int)positionOnTrigger.y, collision.gameObject.GetComponent<Card>());
                }
            }
        }
        onTriggerFirst = false;
    }

}

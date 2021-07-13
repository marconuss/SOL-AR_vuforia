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
            Debug.Log(other.gameObject.name + " on " + this.gameObject.name);
            CardsOnTrigger.Add(other.gameObject);
            CardManager.instance.PlaceCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, other.gameObject.GetComponent<Card>());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cards")
        {
            //Debug.Log("Card placed");
            if (this.gameObject.tag != "ZoomTrigger")
            {
                //Debug.Log(collision.gameObject.name + " on " + this.gameObject.name);
                CardsOnTrigger.Add(collision.gameObject);
                CardManager.instance.PlaceCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, collision.gameObject.GetComponent<Card>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Card placed");
        if (collision.gameObject.tag == "Cards")
        {
            if (this.gameObject.tag != "ZoomTrigger")
            {
                CardsOnTrigger.Remove(collision.gameObject);
                CardManager.instance.RemoveCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, collision.gameObject.GetComponent<Card>());
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
    }
}

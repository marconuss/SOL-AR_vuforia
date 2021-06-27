using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CardsOnTrigger;

    public List<GameObject> GetCardsOnTrigger()
    {
        return CardsOnTrigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cards")
        {
            Debug.Log(other.gameObject.name + " on " + this.gameObject.name);
            CardsOnTrigger.Add(other.gameObject);
            CardManager.instance.PlaceCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, other.gameObject.GetComponent<Card>());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        CardsOnTrigger.Remove(other.gameObject);
        CardManager.instance.RemoveCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, other.gameObject.GetComponent<Card>());
       
    }
}

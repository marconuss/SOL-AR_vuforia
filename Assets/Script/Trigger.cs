using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CardsOnTrigger;

    public Vector2 posInGrid;

    public List<GameObject> GetCardsOnTrigger()
    {
        return CardsOnTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cards")
        {
            Debug.Log(collision.gameObject.name + " on " + this.gameObject.name);
            CardsOnTrigger.Add(collision.gameObject);
            CardManager.instance.PlaceCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x, (int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, collision.gameObject.GetComponent<Card>());
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CardsOnTrigger.Remove(collision.gameObject);
        CardManager.instance.RemoveCard((int)TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).x,(int) TriggerGrid.instance.GetTriggerGridPosition(this.gameObject).y, collision.gameObject.GetComponent<Card>());
    }
}

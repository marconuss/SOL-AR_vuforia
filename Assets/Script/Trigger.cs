using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CardsOnTrigger;

    public Vector2 posInGrid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cards")
        {
            Debug.Log(collision.gameObject.name + " on " + this.gameObject.name);
            CardsOnTrigger.Add(collision.gameObject);
            //CardManager.instance.PlaceCard((int)posInGrid.x, (int)posInGrid.y, collision.gameObject.GetComponent<Card>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CardsOnTrigger.Remove(collision.gameObject);
    }

    public List<GameObject> GetCardsOnTrigger()
    {
        return CardsOnTrigger;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{

    private GameObject zoomObject;
    private SpriteRenderer spriteR;

    void Start()
    {
        zoomObject = GameObject.FindGameObjectWithTag("ZoomImage");

        spriteR = zoomObject.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int cardIndex = (int)collision.gameObject.GetComponent<Card>().type;

        GameManager.instance.fungusManager.SetIntegerVariable("cardOnZoom", cardIndex);

        GameManager.instance.fungusManager.ExecuteBlock("ZoomInHelper");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.fungusManager.ExecuteBlock("ZoomOutHelper");
    }
}

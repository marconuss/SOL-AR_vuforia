using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomTrigger : MonoBehaviour
{

    private GameObject zoomObject;

    void Start()
    {
        zoomObject = GameObject.FindGameObjectWithTag("ZoomImage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!GameManager.instance.secondPhase)
        {
            int cardIndex = (int)collision.gameObject.GetComponent<Card>().type;

            GameManager.instance.fungusManager.SetIntegerVariable("cardOnZoom", cardIndex);

            GameManager.instance.fungusManager.ExecuteBlock("ZoomInHelper");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.fungusManager.ExecuteBlock("ZoomOutHelper");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TriggerTest : MonoBehaviour
{
    public bool cardPlaced = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Card placed");
        if(cardPlaced == false)
        cardPlaced = true;
        //TriggerManager.instance.UpdateCardList();
        
    }

   

    private void OnTriggerExit(Collider other)
    {
        if(cardPlaced == true)

        if(other.gameObject.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            cardPlaced = true;
        }

        cardPlaced = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTest : MonoBehaviour
{
    public Text text;
    public GameObject textPrefab;
    int cardCounter = 0;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (cardCounter == 0)
        {
            Debug.LogWarning("Card played!!");
            text.gameObject.SetActive(true);
            cardCounter++;
        }
        else
        Instantiate(textPrefab, FindObjectOfType<Canvas>().gameObject.transform);
        
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
        
    }
}

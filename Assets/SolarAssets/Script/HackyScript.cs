using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HackyScript : MonoBehaviour
{
    [SerializeField]
    public List<Card> cardsToPlay;
    [SerializeField]
    public List<Vector2Int> positions;

    int pressCounter;

    // Start is called before the first frame update
    void Start()
    {
        pressCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(pressCounter == 4)
            {
                CardManager.instance.RemoveCard(positions[pressCounter].x, positions[pressCounter].y, cardsToPlay[pressCounter]);
                pressCounter++;
            }

            else
            {
                CardManager.instance.PlaceCard(positions[pressCounter].x, positions[pressCounter].y, cardsToPlay[pressCounter]);

                pressCounter++;
            }
            
        }
    }
}

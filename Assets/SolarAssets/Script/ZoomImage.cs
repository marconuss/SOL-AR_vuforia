using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomImage : MonoBehaviour
{
    //instantiate prefabs to animate on the zoom image with random position and sizes
    //also cases according to fungus variables and the zoomed image

    public Card.cardType type;

    public Card.cardType GetCardType()
    {
        return type;
    }
    
}

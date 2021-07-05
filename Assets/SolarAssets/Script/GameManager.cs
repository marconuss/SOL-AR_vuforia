using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite ReflectorSprite;
    public Sprite ConductorSprite;
    public Sprite ConductorGridSprite;
    public Sprite NTypeSiliconSprite;
    public Sprite PTypeSiliconSprite;
    public Sprite GlassSprite;

    public enum photonState {Pass, Blocked, Reflected}

    public static GameManager instance;

    bool circuitActive = false;
    bool electricFieldActive = false;

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void OnEnable()
    {
        CardManager.OnCardPlaced += UpdateInteraction;
    }

    public void UpdateInteraction()
    {
        foreach (Card card in CardManager.instance.cardsOnField)
        {
            if(card.type == Card.cardType.Conductor)
            {
                if (card.neighbourCards[1])
                {
                    if (card.neighbourCards[1].type == Card.cardType.NTypeSilicon || card.neighbourCards[1].type == Card.cardType.PTypeSilicon)
                    {
                        ActivateCircuit();
                    }
                }

                if (card.neighbourCards[3])
                {
                    if (card.neighbourCards[3].type == Card.cardType.NTypeSilicon || card.neighbourCards[3].type == Card.cardType.PTypeSilicon)
                    {
                        ActivateCircuit();
                    }
                }

            }
            if (card.type == Card.cardType.GridConductor)
            {
                if (card.neighbourCards[1])
                {
                    if (card.neighbourCards[1].type == Card.cardType.NTypeSilicon || card.neighbourCards[1].type == Card.cardType.PTypeSilicon)
                    {
                        ActivateCircuit();
                    }
                }

                if (card.neighbourCards[3])
                {
                    if (card.neighbourCards[3].type == Card.cardType.NTypeSilicon || card.neighbourCards[3].type == Card.cardType.PTypeSilicon)
                    {
                        ActivateCircuit();
                    }
                }
            }
            if (card.type == Card.cardType.NTypeSilicon)
            {
                if(card.neighbourCards[1] != null && card.neighbourCards[1].type == Card.cardType.PTypeSilicon) ActivateElectricField();
                if (card.neighbourCards[3] != null && card.neighbourCards[3].type == Card.cardType.PTypeSilicon) ActivateElectricField();


            }
            if (card.type == Card.cardType.PTypeSilicon)
            {
                //if (card.neighbourCards[1] != null && card.neighbourCards[1].type == Card.cardType.NTypeSilicon) ActivateElectricField();
                //if (card.neighbourCards[3] != null && card.neighbourCards[3].type == Card.cardType.NTypeSilicon) ActivateElectricField();
            }

        }
    }

    public void ActivateCircuit()
    {
        if(circuitActive == false) circuitActive = true;

        Debug.Log("Circuit Activated");
    }

    public void ActivateElectricField()
    {
        if (electricFieldActive == false) electricFieldActive = true;
        Debug.Log("Electric Field activated");
    }

    public void ChangePhotonState()
    {
        //change particle direction or animation stuff here
    }

    public void ChangeElectronState()
    {
        if (electricFieldActive == false) { }
            //electrons loose
        if (electricFieldActive && circuitActive == false) { }
                // electrons move and then realign
        if (electricFieldActive && circuitActive) { }
            //electrons travel to P type layer
    }

    private void OnDisable()
    {
        CardManager.OnCardPlaced -= UpdateInteraction;
    }
}

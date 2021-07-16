using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public Sprite[] GlassSprite;
    public Sprite[] ReflectorSprite;
    public Sprite[] ConductorGridSprite;
    public Sprite[] NTypeSiliconSprite;
    public Sprite[] PTypeSiliconSprite;
    public Sprite[] ConductorSprite;

    public GameObject electronPrefab;


    public List<Card.cardType> solution;
    public bool secondPhase;

    public Flowchart fungusManager;

    public enum photonState { Pass, Blocked, Reflected }

    public static GameManager instance;

    bool circuitActive = false;
    bool electricFieldActive = false;

    int correctTiles;


    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        correctTiles = 0;
        secondPhase = false;

    }

    private void OnEnable()
    {
        CardManager.OnCardPlaced += UpdateInteraction;
        CardManager.OnCardPlaced += CheckSolution;
        CardManager.OnCardRemoved += UpdateInteraction;
        CardManager.OnCardRemoved += CheckSolution;
    }

    public void UpdateInteraction()
    {
        foreach (Card card in CardManager.instance.cardsOnField)
        {
            if (card.type == Card.cardType.Conductor)
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
                    else if (circuitActive) DeactivateCircuit();
                }
                
            }
            if (card.type == Card.cardType.NTypeSilicon)
            {
                if (card.neighbourCards[1] != null && card.neighbourCards[1].type == Card.cardType.PTypeSilicon) ActivateElectricField();
                else if (electricFieldActive) DeactivateElectricField();
                if (card.neighbourCards[3] != null && card.neighbourCards[3].type == Card.cardType.PTypeSilicon) ActivateElectricField();
                else if (electricFieldActive) DeactivateElectricField();
            }

        }
    }

    public void CheckSolution()
    {
        correctTiles = 0;

        if (!secondPhase)
        {
            foreach (Card card in CardManager.instance.cardsOnField)
            {
                if (card.type == solution[card.gridPosition.y] && card.gridPosition.x == 0)
                {
                    correctTiles++;

                    if (correctTiles == 6 && secondPhase == false)
                    {
                        ActivateSecondPhase();
                        return;
                    }

                    if (correctTiles == 6 && secondPhase == true)
                    {
                        Win();
                        return;
                    }

                }
            }
        }

        if (secondPhase)
        {
            foreach (Card card in CardManager.instance.cardsOnField)
            {
                if (card.type == solution[card.gridPosition.y] && card.gridPosition.x == 1)
                {
                    correctTiles++;

                    if (correctTiles == 6 && secondPhase == false)
                    {
                        ActivateSecondPhase();
                        return;
                    }

                    if (correctTiles == 6 && secondPhase == true)
                    {
                        Win();
                        return;
                    }

                }
            }
        }


    }

    void ActivateSecondPhase()
    {
        secondPhase = true;
        correctTiles = 0;
        Debug.Log("Second Phase started");

 
        CardManager.instance.CreateNewGrid(CardManager.instance.columns + 1, CardManager.instance.rows, new Vector3(CardManager.instance.cellSize.x / 2, CardManager.instance.cellSize.y));
        CardManager.instance.RemoveAllCards();
        SolveFirstColumn();

    }

    void SolveFirstColumn()
    {
        for (int i = 0; i < solution.Count; i++)
        {
            Card newCard = Instantiate(new GameObject()).AddComponent<Card>();
            newCard.type = solution[i];

            CardManager.instance.PlaceCard(0, i, newCard);
        }
    }

    void Win()
    {
        Debug.Log("You won the game wohoo!");
    }

    public void ActivateCircuit()
    {
        if (circuitActive == false) circuitActive = true;

        Debug.Log("Circuit Activated");
    }
    public void DeactivateCircuit()
    {
        if (circuitActive == true) circuitActive = false;

        Debug.Log("Circuit Activated");
    }

    public void ActivateElectricField()
    {
        if (electricFieldActive == false) electricFieldActive = true;
        //set fungus variable
        fungusManager.SetBooleanVariable("electricField", true);
        //Debug.Log("Electric Field activated");
    }
    public void DeactivateElectricField()
    {
        if (electricFieldActive == true) electricFieldActive = false;
        //set fungus variable
        fungusManager.SetBooleanVariable("electricField", false);
        //Debug.Log("Electric Field activated");
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
        CardManager.OnCardPlaced -= UpdateInteraction;
        CardManager.OnCardPlaced -= CheckSolution;
        CardManager.OnCardRemoved -= UpdateInteraction;
        CardManager.OnCardRemoved -= CheckSolution;
    }
}


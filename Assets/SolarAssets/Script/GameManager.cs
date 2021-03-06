using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    [Header("Card Sprites")]
    public Sprite[] GlassSprite;
    public Sprite[] ReflectorSprite;
    public Sprite[] ConductorGridSprite;
    public Sprite[] NTypeSiliconSprite;
    public Sprite[] PTypeSiliconSprite;
    public Sprite[] ConductorSprite;

    [Header("Electrons")]
    public GameObject electronPrefab;
    public GameObject electronParent;

    [Header("ElectricField")]
    public GameObject electricFieldPrefab;
    public GameObject electricFieldSmall;

    [Header("Solution")]
    public List<Card.cardType> solution;
    public bool secondPhase;

    [Header("Fungus")]
    public Flowchart fungusManager;

    enum photonState { Pass, Blocked, Reflected }

    public static GameManager instance;

    [Header("Interactions")]
    public bool circuitActive = false;
    public bool electricFieldActive = false;

    int correctTiles;

    [HideInInspector]
    public List<ElectronBehavior> allElectrons;


    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        correctTiles = 0;
        secondPhase = false;
        allElectrons = new List<ElectronBehavior>();

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
            if (card.type == Card.cardType.GridConductor)
            {
                //does for below neighbour exist
                if (card.neighbourCards[3])
                {
                    //is neighbour below ntype?
                    if (card.neighbourCards[3].type == Card.cardType.NTypeSilicon)
                    {
                        //set this neighbourcard to the next checked card
                        Card nextCard = card.neighbourCards[3];

                        //does neighbour for next checked card exist?
                        if (nextCard.neighbourCards[3])
                        {
                            //is next checked card of type psilicon?
                            if (nextCard.neighbourCards[3].type == Card.cardType.PTypeSilicon)
                            {
                                nextCard = nextCard.neighbourCards[3];
                                
                                if(nextCard.neighbourCards[3])
                                {
                                    if(nextCard.neighbourCards[3].type == Card.cardType.Conductor)
                                    {
                                        ActivateCircuit();
                                        break;
                                    }
                                    else if (circuitActive) DeactivateCircuit();
                                }
                                else if (circuitActive) DeactivateCircuit();

                            }
                            else if (circuitActive) DeactivateCircuit();
                        }
                        else if (circuitActive) DeactivateCircuit();
                    }
                    else if (circuitActive) DeactivateCircuit();

                }
                else if (circuitActive) DeactivateCircuit();

            }
            else if (circuitActive) DeactivateCircuit();



            if (card.type == Card.cardType.NTypeSilicon)
            {
                if (card.neighbourCards[1] != null && card.neighbourCards[1].type == Card.cardType.PTypeSilicon) 
                {
                    if (electricFieldActive == false)
                    {
                        Vector3 spawnPos;
                        spawnPos.x = (GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.x);
                        spawnPos.y = (GameObject.FindGameObjectWithTag("PTypeSilicon").transform.position.y + GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.y) / 2;
                        spawnPos.z = 0;
                        ActivateElectricField(spawnPos);
                    }
                }

                else if (card.neighbourCards[3] != null && card.neighbourCards[3].type == Card.cardType.PTypeSilicon) 
                {
                    if (electricFieldActive == false)
                    {
                        Vector3 spawnPos;
                        spawnPos.x = (GameObject.FindGameObjectWithTag("PTypeSilicon").transform.position.x);
                        spawnPos.y = (GameObject.FindGameObjectWithTag("PTypeSilicon").transform.position.y + GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.y) / 2;
                        spawnPos.z = 0;
                        ActivateElectricField(spawnPos);
                    }
                }
                else if (electricFieldActive)
                { 
                    DeactivateElectricField(); 
                }
            }
            if (card.type == Card.cardType.PTypeSilicon)
            {
                if (card.neighbourCards[1] != null && card.neighbourCards[1].type == Card.cardType.NTypeSilicon)
                {
                    if (electricFieldActive == false)
                    {
                        Vector3 spawnPos;
                        spawnPos.x = (GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.x);
                        spawnPos.y = (GameObject.FindGameObjectWithTag("PTypeSilicon").transform.position.y + GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.y) / 2;
                        spawnPos.z = 0;
                        ActivateElectricField(spawnPos);
                    }
                }

                else if (card.neighbourCards[3] != null && card.neighbourCards[3].type == Card.cardType.NTypeSilicon)
                {
                    if (electricFieldActive == false)
                    {
                        Vector3 spawnPos;
                        spawnPos.x = (GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.x);
                        spawnPos.y = (GameObject.FindGameObjectWithTag("PTypeSilicon").transform.position.y + GameObject.FindGameObjectWithTag("NTypeSilicon").transform.position.y) / 2;
                        spawnPos.z = 0;
                        ActivateElectricField(spawnPos);
                    }
                }
                else if (electricFieldActive)
                {
                    DeactivateElectricField();
                }
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
                        fungusManager.StopAllBlocks();
                        fungusManager.ExecuteBlock("StartSecondPhase");
                        //ActivateSecondPhase();
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
                        fungusManager.StopAllBlocks();
                        fungusManager.ExecuteBlock("StartSeceondPhase");
                        //ActivateSecondPhase();
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



    public void ActivateSecondPhase()
    {       
        secondPhase = true;
        correctTiles = 0;
        Debug.Log("Second Phase started");
        DeactivateElectricField();
 
        CardManager.instance.CreateNewGrid(CardManager.instance.columns + 1, CardManager.instance.rows, new Vector3(CardManager.instance.cellSize.x / 2, CardManager.instance.cellSize.y));
        CardManager.instance.RemoveAllCards();

          //foreach (ElectronBehavior electron in allElectrons)
          //{
          //      if (electron.gameObject)
          //      {
          //          allElectrons.Remove(electron);
          //          Destroy(electron.gameObject);
          //      }
          //}
            

        SolveFirstColumn();
        DeactivateCircuit();

    }

    void SolveFirstColumn()
    {
        for (int i = 0; i < solution.Count; i++)
        {
            GameObject cardObject = new GameObject("CardObject");
            Card newCard = Instantiate(cardObject).AddComponent<Card>();
            newCard.type = solution[i];

            CardManager.instance.PlaceCard(0, i, newCard);
            Destroy(newCard.gameObject);
            Destroy(cardObject);
        }
    }

    void Win()
    {
        fungusManager.ExecuteBlock("EndSecondPhase");
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

        Debug.Log("Circuit Deactivated");
    }

    public void ActivateElectricField(Vector3 pos)
    {
        if (electricFieldActive == false) electricFieldActive = true;
        
        if (!secondPhase)
            Instantiate(electricFieldPrefab, pos, Quaternion.identity);

        if (secondPhase)
        {   // is nicht ok 
            Instantiate(electricFieldSmall, new Vector3(12f, pos.y, 0), Quaternion.identity);
        }


        //set fungus variable
        fungusManager.SetBooleanVariable("electricField", true);
        Debug.Log("Electric Field activated");
    }
    public void DeactivateElectricField()
    {
        if (electricFieldActive == true) electricFieldActive = false;

        GameObject electricField = GameObject.FindGameObjectWithTag("ElectricField");
        Destroy(electricField);

        //set fungus variable
        fungusManager.SetBooleanVariable("electricField", false);
        Debug.Log("Electric Field deactivated");
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


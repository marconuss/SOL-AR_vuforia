using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Fungus;

public class CardManager : MonoBehaviour
{
    [Header("Grid Info")]
    public int rows;
    public int columns;
    public Vector2 cellSize;

    [Header("Card Info")]
    public GameObject cardPrefab;

    public List<Card> cardsOnField;

    public static CardManager instance;

    //event that gets called when card is placed
    public delegate void CardPlace();
    public static event CardPlace OnCardPlaced;
    public delegate void CardRemove();
    public static event CardPlace OnCardRemoved;

    GridSystem grid;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;

        CreateNewGrid(columns, rows, cellSize);
        Debug.Log("Card Grid Setup!");
        cardsOnField = new List<Card>();
    }

    public void CreateNewGrid(int _columns, int _rows, Vector2 _cellSize)
    {
        if (grid != null) grid = null;
        grid = new GridSystem(_columns, _rows, _cellSize);
    }

    public void CreateNewGrid(int _columns, int _rows, Vector2 _cellSize, Vector3 _startPos)
    {
        grid = new GridSystem(_columns, _rows, _cellSize, _startPos);
    }

    public Card GetLastCardOnCell(int x, int y)
    {
        return grid.gridArray[x, y].placedCards.Last<Card>();
    }

    public void PlaceCard(int x, int y, Card _card)
    {
        cardsOnField.Add(_card);
        grid.gridArray[x, y].placedCards.Add(_card);

        if (grid.gridArray[x, y].hasCard == false) grid.gridArray[x, y].hasCard = true;
          
        else if (grid.gridArray[x,y].hasCard)
        {
            foreach (Card slotCard in grid.gridArray[x,y].placedCards)
            {
                if(slotCard != _card)
                {
                    slotCard.HideCardSprite();
                }
            }
            
        }
        _card.gridPosition = new Vector2Int(x, y);
        _card.InstantiateCard(grid.GetCellCenter(x, y), cardPrefab);

        //save new placed card position for fungus
        GameManager.instance.fungusManager.SetIntegerVariable("lastCardPos", _card.gridPosition.y);


        foreach (Card card in cardsOnField)
        {
            UpdateNeighbours(card.gridPosition.x, card.gridPosition.y, card);     
        }

        //call fungus flowchart block
        //if(GameManager.instance.secondPhase == false)
        //blocks are executed always at card placement
        GameManager.instance.fungusManager.ExecuteBlock(_card.type.ToString());

        if(OnCardPlaced != null)
        {
            OnCardPlaced();
        } 
    }

    public void RemoveCard(int x, int y, Card _card)
    {
        cardsOnField.Remove(_card);
        grid.gridArray[x, y].placedCards.Remove(_card);

        _card.RemoveCard();

        //check if there are still cards left and if there are, show the most top sprite
        int slotCardCount = grid.gridArray[x, y].placedCards.Count;
        if (slotCardCount > 0)
        {
            grid.gridArray[x, y].placedCards[slotCardCount - 1].ShowCardSprite();
        }
        else grid.gridArray[x, y].hasCard = false;

        foreach (Card card in cardsOnField)
        {
            UpdateNeighbours(card.gridPosition.x, card.gridPosition.y, card);
        }

        if (OnCardRemoved != null)
        {
            OnCardRemoved();
        }
    }

    public void RemoveAllCards()
    {
        foreach (Card card in cardsOnField)
        {
            card.RemoveCard();
        }
    }

    public void UpdateNeighbours(int x, int y, Card _card)
    {
        if (x != 0)
        {
            int cardAmount = grid.gridArray[x - 1, y].placedCards.Count;

            if (cardAmount > 0) //check neighbour to the left
            {
                int index = cardAmount - 1; //last card on the list

                _card.neighbourCards[0] = grid.gridArray[x - 1, y].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell
            }

            if (cardAmount == 0 && _card.neighbourCards[0])
            {
                _card.neighbourCards[0] = null;
            }

        }

        if (x != grid.Width - 1)
        {
            int cardAmount = grid.gridArray[x + 1, y].placedCards.Count;

            if (cardAmount > 0)
            {
                int index = cardAmount - 1; //last card on the list

                _card.neighbourCards[2] = grid.gridArray[x + 1, y].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell

            }

            if (cardAmount == 0 && _card.neighbourCards[2])
            {
                _card.neighbourCards[2] = null;
            }
        }
        

        if (y != 0) 
        {
            int cardAmount = grid.gridArray[x, y - 1].placedCards.Count;

            if (cardAmount > 0)
            {
                int index = cardAmount - 1; //index of last card on the list

                _card.neighbourCards[3] = grid.gridArray[x, y - 1].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell
            }

            if (cardAmount == 0 && _card.neighbourCards[3])
            {
                _card.neighbourCards[3] = null;
            }
        }
        
        if( y != grid.Height - 1)
        {
            int cardAmount = grid.gridArray[x, y + 1].placedCards.Count;

            if (cardAmount > 0)
            {
                int index = cardAmount - 1; //index of last card on the list

                _card.neighbourCards[1] = grid.gridArray[x, y + 1].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell
            }

            if (cardAmount == 0 && _card.neighbourCards[1])
            {
                _card.neighbourCards[1] = null;
            }


        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardManager : MonoBehaviour
{
    public int rows, columns;
    public float cellSize;

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

        grid = new GridSystem(columns, rows, cellSize);
        Debug.Log("Card Grid Setup!");
        cardsOnField = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Card GetLastCardOnCell(int x, int y)
    {
        return grid.gridArray[x, y].placedCards.Last<Card>();
    }

    public void PlaceCard(int x, int y, Card _card)
    {
        cardsOnField.Add(_card);
        grid.gridArray[x, y].placedCards.Add(_card);
        grid.gridArray[x, y].hasCard = true;
        _card.gridPosition = new Vector2Int(x, y);
        _card.InstantiateCard(grid.GetCellCenter(x, y));

        foreach (Card card in cardsOnField)
        {
            UpdateNeighbours(card.gridPosition.x, card.gridPosition.y, card);     
        }

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


        foreach (Card card in cardsOnField)
        {
            UpdateNeighbours(card.gridPosition.x, card.gridPosition.y, card);
        }

        if (OnCardRemoved != null)
        {
            OnCardRemoved();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Fungus;

public class CardManager : MonoBehaviour
{
    public int rows, columns;
    public Vector2 cellSize;

    public GameObject cardPrefab;
    public GameObject smallCardPrefab;

    public List<Card> cardsOnField;

    public static CardManager instance;

    //event that gets called when card is placed
    public delegate void CardPlace();
    public static event CardPlace OnCardPlaced;
    public delegate void CardRemove();
    public static event CardPlace OnCardRemoved;

    public Flowchart fungusManager;

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

    // Update is called once per frame
    void Update()
    {
       
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
        grid.gridArray[x, y].hasCard = true;
        _card.gridPosition = new Vector2Int(x, y);

        if(!GameManager.instance.secondPhase)
        _card.InstantiateCard(grid.GetCellCenter(x, y), cardPrefab);
        else if(GameManager.instance.secondPhase)
        _card.InstantiateCard(grid.GetCellCenter(x, y), smallCardPrefab);


        foreach (Card card in cardsOnField)
        {
            UpdateNeighbours(card.gridPosition.x, card.gridPosition.y, card);     
        }

        if(OnCardPlaced != null)
        {
            OnCardPlaced();
        }


        //call fungus flowchart block
        //fungusManager.ExecuteBlock(_card.type.ToString());
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

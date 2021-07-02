using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite ReflectorSprite;
    public Sprite ConductorSprite;
    public Sprite ConductorGridSprite;
    public Sprite NTypeSiliconSprite;
    public Sprite PTypeSiliconSprite;
    public Sprite GlassSprite;
    
    public int rows, columns;
    public float cellSize;
    public static CardManager instance;

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
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlaceCard(int x, int y, Card _card)
    {
        grid.gridArray[x, y].placedCards.Add(_card);
        grid.gridArray[x, y].hasCard = true;
        UpdateNeighbours(x, y, _card);
        _card.InstantiateCard(grid.GetCellCenter(x, y));
    }

    public void RemoveCard(int x, int y, Card _card)
    {
        grid.gridArray[x, y].placedCards.Remove(_card);
        _card.RemoveCard();
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
           
        }

        if (x != grid.Width - 1)
        {
            int cardAmount = grid.gridArray[x + 1, y].placedCards.Count;

            if (cardAmount > 0)
            {
                int index = cardAmount - 1; //last card on the list

                _card.neighbourCards[2] = grid.gridArray[x + 1, y].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell

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
        }
        
        if( y != grid.Height - 1)
        {
            int cardAmount = grid.gridArray[x, y + 1].placedCards.Count;

            if (cardAmount > 0)
            {
                int index = cardAmount - 1; //index of last card on the list

                _card.neighbourCards[1] = grid.gridArray[x, y + 1].placedCards[index]; //set left neighbour to last card on the list in the cell left from this card's cell
            }
        }
    }
}

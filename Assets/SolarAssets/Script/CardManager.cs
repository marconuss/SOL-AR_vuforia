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
        _card.InstantiateCard(grid.GetCellCenter(x, y));
    }

    public void RemoveCard(int x, int y, Card _card)
    {
        grid.gridArray[x, y].placedCards.Remove(_card);
        _card.RemoveCard();
        
    }
}

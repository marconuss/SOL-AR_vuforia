using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject cardPrefab;

    public static CardManager instance;

    public Card card;

    GridSystem grid;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;

        grid = new GridSystem(3, 3, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlaceCard(0, 0, card);
        }
    }

    public void PlaceCard(int x, int y, Card _card)
    {
        grid.gridArray[x, y].placedCards.Add(_card);
        grid.gridArray[x, y].hasCard = true;
        _card.InstantiateCard(grid.GetCellCenter(x, y));
    }
}

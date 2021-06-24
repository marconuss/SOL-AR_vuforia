using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject[] triggers;

    private GameObject[,] triggerGrid;

    public int rows = 6;
    public int collumns = 2;

    private void Awake()
    {
        triggers = GameObject.FindGameObjectsWithTag("Trigger");
        triggers = triggers.OrderBy(t => t.transform.GetSiblingIndex()).ToArray();

        //foreach(GameObject t in triggers)
        //{
        //    Debug.Log(t.name);
        //}
        SetUpTriggerGrid();
    }

    public List<GameObject> GetCardsOnTrigger(int x, int y)
    {
        return triggerGrid[x, y].GetComponent<Trigger>().GetCardsOnTrigger();
    }

    public void UpdateCards()
    {
        for (int x = 0; x < collumns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (GetCardsOnTrigger(x, y).Count != 0)
                {
                    foreach (GameObject card in GetCardsOnTrigger(x, y))
                    {
                        Debug.Log(card.name + " on " + triggerGrid[x, y].name);
                    }
                }
            }
        }
    }

    private void SetUpTriggerGrid()
    {
        int triggerIndex = 0;
        triggerGrid = new GameObject[collumns, rows];

        for (int x = 0; x < collumns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                triggerGrid[x, y] = triggers[triggerIndex];
                Debug.Log(triggerGrid[x, y].name + "with position " + x + ", " + y + " with index " + triggerIndex);
                triggerIndex++;
            }
        }
    }

    private Vector2 GetTriggerGridPosition(GameObject trigger)
    {
        Vector2 trigPos = new Vector2(0, 0);

        for (int x = 0; x < collumns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (triggerGrid[x, y] == trigger)
                {
                    trigPos = new Vector2(x, y);
                }
            }
        }

        return trigPos;
    }

}

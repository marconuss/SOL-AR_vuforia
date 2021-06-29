using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    public CardSlot[,] gridArray;
    float cellSize;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new CardSlot[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = new CardSlot();
                Debug.Log(x + "," + y);
                //Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x + 1, y), Color.red, 10f);
                //Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x, y + 1), Color.red, 10f);
                GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = GetCellCenter(x, y);
            }

        }

        //Debug.DrawLine(CellToWorldPosition(0, height), CellToWorldPosition(width, height), Color.red, 100f);
        //Debug.DrawLine(CellToWorldPosition(width, 0), CellToWorldPosition(width, height), Color.red, 100f);
    }

    public Vector3 CellToWorldPosition(int x, int y)
    {
        return new Vector3(x * cellSize, y * cellSize, 1);
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        return new Vector3(x * cellSize + cellSize / 2, y * cellSize + cellSize /2, 1);
    }

}

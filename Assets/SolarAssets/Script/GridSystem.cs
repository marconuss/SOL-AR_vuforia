using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    int width;
    int height;
    public CardSlot[,] gridArray;
    Vector2 cellSize;

    public int Width { get => width;}
    public int Height { get => height;}

    public GridSystem(int width, int height, Vector2 cellSize)
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
                Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x + 1, y), Color.red, 100f);
                Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x, y + 1), Color.red, 100f);
                //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = GetCellCenter(x, y);
            }

        }

        Debug.DrawLine(CellToWorldPosition(0, height), CellToWorldPosition(width, height), Color.red, 100f);
        Debug.DrawLine(CellToWorldPosition(width, 0), CellToWorldPosition(width, height), Color.red, 100f);
    }

    public GridSystem(int width, int height, Vector2 cellSize, Vector3 startPos)
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
                Debug.DrawLine(CellToWorldPosition(x, y) + startPos, CellToWorldPosition(x + 1, y) + startPos, Color.red, 10f);
                Debug.DrawLine(CellToWorldPosition(x, y) + startPos, CellToWorldPosition(x, y + 1) + startPos, Color.red, 10f);
                GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = GetCellCenter(x, y) + startPos;
            }

        }

        Debug.DrawLine(CellToWorldPosition(0, height) + startPos, CellToWorldPosition(width, height) + startPos, Color.red, 100f);
        Debug.DrawLine(CellToWorldPosition(width, 0) + startPos, CellToWorldPosition(width, height) + startPos, Color.red, 100f);
    }

    public Vector3 CellToWorldPosition(int x, int y)
    {
        return new Vector3(x * cellSize.x, y * cellSize.y, 1);
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        return new Vector3(x * cellSize.x + cellSize.x / 2, y * cellSize.y + cellSize.y /2, 1);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript
{
    private int cellCount;
    private int[,] table;
    private int emptyIndexX;
    private int emptyIndexY;

    public int EmptyIndexX
    {
        get
        {
            return emptyIndexX;
        }

        set
        {
            emptyIndexX = value;
        }
    }

    public int EmptyIndexY
    {
        get
        {
            return emptyIndexY;
        }

        set
        {
            emptyIndexY = value;
        }
    }

    public int[,] Table
    {
        get
        {
            return table;
        }

        set
        {
            table = value;
        }
    }

    public ModelScript(int _cellCount)
    {
        cellCount = _cellCount;
        Table = new int[cellCount, cellCount];
        FillTable();
    }

    private void FillTable()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < cellCount * cellCount; i++)
        {
            int x = i;
            temp.Add(x);
        }

        for (int i = 0; i < cellCount; i++)
        {
            for (int j = 0; j < cellCount; j++)
            {
                int cell = Random.Range(0, temp.Count);
                int x = temp[cell];
                temp.RemoveAt(cell);
                Table[i, j] = x;
                if (x == (cellCount * cellCount - 1))
                {
                    EmptyIndexX = i;
                    EmptyIndexY = j;
                }
            }
        }
    }

    public void Move(int indexX, int indexY)
    {
        int temp = Table[indexX, indexY];
        Table[indexX, indexY] = Table[EmptyIndexX, EmptyIndexY];
        Table[EmptyIndexX, EmptyIndexY] = temp;
        EmptyIndexX = indexX;
        EmptyIndexY = indexY;
    }

    public bool IfCanMove(int indexX, int indexY)
    {
        if (indexX - 1 >= 0)
        {
            if (Table[indexX - 1, indexY] == (cellCount * cellCount - 1))
                return true;
        }
        if (indexX + 1 < cellCount)
        {
            if (Table[indexX + 1, indexY] == (cellCount * cellCount - 1))
                return true;
        }
        if (indexY - 1 >= 0)
        {
            if (Table[indexX, indexY - 1] == (cellCount * cellCount - 1))
                return true;
        }
        if (indexY + 1 < cellCount)
        {
            if (Table[indexX, indexY + 1] == (cellCount * cellCount - 1))
                return true;
        }
        return false;
    }

    public bool IsEnd()
    {
        for (int i = 0; i < cellCount; i++)
        {
            for (int j = 0; j < cellCount; j++)
            {
                if (Table[i, j] != j * cellCount + i)
                    return false;
            }
        }
        return true;
    }
}

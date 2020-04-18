using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

using System;

public class Grid<TGridObject> 
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;



    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width, height;
    
    float cellSize;
    Vector3 originPosition;

    bool showDebug;

    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>,int, int, TGridObject> createGridObject, bool debug = false)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        this.showDebug = debug;

        gridArray = new TGridObject[width, height];

        for (int  x = 0;  x < gridArray.GetLength(0);  x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this,x,y);
            }
        }


        //bool showDebug = false;

        if (showDebug)
        {

            debugTextArray = new TextMesh[width, height];



            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {

                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 22, Color.white, TextAnchor.MiddleCenter, TextAlignment.Left, 5000, 0.2f);

                    DL(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f, false);
                    DL(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f, false);


                }



            }

            DL(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f, false);
            DL(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f, false);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
            {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
       
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
        //return new Vector3(x, y) * cellSize;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void DL (Vector3 pos1, Vector3 pos2, Color color, float duration, bool inFront)
    {
        //wrapper to offset the parent transform into the line drawing

        Debug.DrawLine(pos1 , pos2 , color, duration, inFront);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height )
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }


    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    //specific for heatmap
    //public void AddValue(int x, int y, int value)
    //{
    //    SetValue(x, y, GetValue(x, y) + value);
    //}


    public TGridObject GetGridObject( int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject( Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    //specific for heatmap
    //public void AddValue(Vector3 worldPosition, int value , int fullValueRange, int totalRange)
    //{
    //    int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        
    //    GetXY(worldPosition, out int originX, out int originY);
    //    for (int x = 0; x < totalRange; x++)
    //    {
    //        for (int y = 0; y < totalRange - x; y++)
    //        {
    //            int radius = x + y;

    //            int addValueAmount = value;

    //            if (radius > fullValueRange)
    //            {
    //                addValueAmount -= lowerValueAmount * (radius - fullValueRange);
    //            }

    //            AddValue(originX + x, originY + y, addValueAmount);
                
    //            if (x != 0)
    //            {
    //                AddValue(originX - x, originY + y, addValueAmount);
    //            }

    //            if (y != 0)
    //            {
    //                AddValue(originX + x, originY - y, addValueAmount);


    //                if (x != 0)
    //                {
    //                    AddValue(originX - x, originY - y, addValueAmount);
    //                }
    //            }

    //        }
    //    }

        
    //}

}

using UnityEngine;
public class GridModel<TGridObject>
{

    public Vector2Int Size { get; private set; }
    public float CellSize { get; private set; }

    public TGridObject[,] Array { get; private set; } 

    public Vector2 PositionOrigin { get; private set; }

    public GridModel(Vector2Int size, float cellSize, Vector2 positionOrigin)
    {
        Size = size;
        CellSize = cellSize;

        PositionOrigin = positionOrigin;

        Array = new TGridObject[size.x, size.y];
    }

    public Vector3 ConvertingPosition(Vector2 XY)
    {
        return new Vector3(XY.x, XY.y, 0) * CellSize + (Vector3)PositionOrigin;
    }

    public Vector2Int ConvertingPosition(Vector3 worldPose)
    {
        int X = Mathf.FloorToInt((worldPose - (Vector3)PositionOrigin).x / CellSize);
        int Y = Mathf.FloorToInt((worldPose - (Vector3)PositionOrigin).y / CellSize);
        return new Vector2Int(X, Y);
    }
    public void SetValueInGrid(Vector2Int XY, TGridObject value)
    {
        if (XY is { x: >= 0, y: >= 0 } && XY.x < Size.x && XY.y < Size.y)
        {
            Array[XY.x, XY.y] = value;
        }
    }

}

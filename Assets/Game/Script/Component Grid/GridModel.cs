using UnityEngine;
public class GridModel<TGridObject>
{

    public Vector2Int Size { get; private set; }
    public float CellSize { get; private set; }
    
    public TGridObject[,] Array { get; private set; }

    private Vector2 _positionOrigin;

    public GridModel(Vector2Int size, float cellSize, Vector2 positionOrigin)
    {
        Size = size;
        CellSize = cellSize;

        _positionOrigin = positionOrigin;

        Array = new TGridObject[size.x, size.y];
    }

    public Vector3 GetWorldPose(Vector2 XY)
    {
        return new Vector3(XY.x, XY.y, 0) * CellSize + (Vector3)_positionOrigin;
    }

    public Vector2Int GetGridPose(Vector3 worldPose)
    {
        int X = Mathf.FloorToInt((worldPose - (Vector3)_positionOrigin).x / CellSize);
        int Y = Mathf.FloorToInt((worldPose - (Vector3)_positionOrigin).y / CellSize);
        return new Vector2Int(X, Y);
    }
    public void SetValueInGrid(Vector2Int XY, TGridObject value)
    {
        if (XY.x >= 0 && XY.y >= 0 && XY.x < Size.x && XY.y < Size.y)
        {
            Array[XY.x, XY.y] = value;
        }
    }
    public void SetValueInWorld(Vector3 worldPose, TGridObject value)
    {
        SetValueInGrid(GetGridPose(worldPose), value);
    }

    public TGridObject GetValueInGrid(Vector2Int XY)
    {
        if (XY.x >= 0 && XY.y >= 0 && XY.x < Size.x && XY.y < Size.y)
        {
            return Array[XY.x, XY.y];
        }
        
        return default;

    }
    public TGridObject GetValueInWorld(Vector3 worldPose)
    {
        return GetValueInGrid(GetGridPose(worldPose));
    }
}

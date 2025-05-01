using Game.Script.Component_Grid.Component_Pathfind;
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
    public void SetValue(Vector2Int XY, TGridObject value)
    {
        if (XY is { x: >= 0, y: >= 0 } && XY.x < Size.x && XY.y < Size.y)
        {
            Array[XY.x, XY.y] = value;
        }
    }

}

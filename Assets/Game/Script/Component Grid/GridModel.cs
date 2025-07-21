using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;
public class GridModel
{

    public Vector2Int Size { get; private set; }
    public float CellSize { get; private set; }

    public GridNode[,] Array { get; private set; }

    public Vector2 PositionOrigin { get; private set; }

    public GridModel(Vector2Int size, float cellSize, Vector2 positionOrigin)
    {
        Size = size;
        CellSize = cellSize;

        PositionOrigin = positionOrigin;

        Array = new GridNode[size.x, size.y];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Array[x, y] = new GridNode(new Vector2Int(x, y));
            }
        }
    }

}

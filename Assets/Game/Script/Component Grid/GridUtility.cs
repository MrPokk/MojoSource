using System;
using UnityEngine;
public abstract class GridUtility
{

    public static T GetNodeByIndex<T>(Vector2Int index, T[,] grid)
    {
        return grid[index.x, index.y];
    }


    public static bool IsWithinGrid<T>(Vector2Int indexNode, T[,] grid)
    {
        return indexNode.x >= 0 && indexNode.x < grid.GetLength(0) && indexNode.y >= 0 && indexNode.y < grid.GetLength(1);
    }

    private static bool IsWithinGrid<T>(Vector2Int indexNode, GridModel<T> grid)
    {
        return indexNode.x >= 0 && indexNode.x < grid.Size.x && indexNode.y >= 0 && indexNode.y < grid.Size.y;
    }

    public static Vector3? TryGetPositionInGrid<T>(Vector2Int indexNode, GridModel<T> grid)
    {
        if (IsWithinGrid(indexNode, grid))
            return new Vector3(indexNode.x, indexNode.y, 0) * grid.CellSize + (Vector3)grid.PositionOrigin;

        Debug.LogWarning("ERROR: Invalid grid position");
        return null;
    }

    public static Vector2Int? TryGetPositionInGrid<T>(Vector3 objectPosition, GridModel<T> grid)
    {
        if (!IsWithinGrid(grid.ConvertingPosition(objectPosition), grid))
        {
            Debug.LogWarning("ERROR: Invalid grid position");
            return null;
        }

        int X = Mathf.FloorToInt((objectPosition - (Vector3)grid.PositionOrigin).x / grid.CellSize);
        int Y = Mathf.FloorToInt((objectPosition - (Vector3)grid.PositionOrigin).y / grid.CellSize);
        return new Vector2Int(X, Y);
    }

}

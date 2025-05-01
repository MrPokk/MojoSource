using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;
public static class GridUtility
{
    public static Vector2Int GridMousePosition {
        get {
            var IsGrid = TryGetPositionInGrid(MouseInteraction.MousePose, out var mousePosition);
            if (IsGrid)
                return mousePosition;

            return Vector2Int.one * int.MinValue;
        }
    }
    public static GridModel<GridNode> GridModel => GameData<Main>.Boot.GridController.Grid;


    public static void SetValueInGrid(Vector2Int index, GridNode value)
    {
        if (IsWithinGrid(index))
        {
            GridModel.Array[index.x, index.y] = value;
        }
    }
    public static void SetValueInGrid<T>(Vector2Int index, T[,] grid, T value)
    {
        if (IsWithinGrid(index))
        {
            grid[index.x, index.y] = value;
        }
    }
    
    public static void SetTypeInGrid(Vector2Int index, GridNode.TypeNode value)
    {
        if (IsWithinGrid(index))
        {
            GridModel.Array[index.x, index.y].Type = value;
        }
    }
    
    public static T GetNodeByIndex<T>(Vector2Int index, T[,] grid)
    {
        return grid[index.x, index.y];
    }
    public static GridNode GetNodeByIndex(Vector2Int index)
    {
        return GridModel.Array[index.x, index.y];
    }
    public static Vector3 ConvertingPosition(Vector2 XY)
    {
        return new Vector3(XY.x, XY.y, 0) * GridModel.CellSize + (Vector3)GridModel.PositionOrigin;
    }
    public static Vector2Int ConvertingPosition(Vector3 worldPose)
    {
        int X = Mathf.FloorToInt((worldPose - (Vector3)GridModel.PositionOrigin).x / GridModel.CellSize);
        int Y = Mathf.FloorToInt((worldPose - (Vector3)GridModel.PositionOrigin).y / GridModel.CellSize);
        return new Vector2Int(X, Y);
    }

    public static bool IsWithinGrid(Vector2Int indexNode)
    {
        return indexNode.x >= 0 && indexNode.x < GridModel.Size.x && indexNode.y >= 0 && indexNode.y < GridModel.Size.y;
    }

    public static bool IsWithinGrid<T>(Vector2Int indexNode, T[,] grid)
    {
        return indexNode.x >= 0 && indexNode.x < grid.GetLength(0) && indexNode.y >= 0 && indexNode.y < grid.GetLength(1);
    }

    private static bool IsWithinGrid<T>(Vector2Int indexNode, GridModel<T> grid)
    {
        return indexNode.x >= 0 && indexNode.x < grid.Size.x && indexNode.y >= 0 && indexNode.y < grid.Size.y;
    }
    public static bool TryGetPositionInGrid(Vector2Int indexNode, out Vector3 positionValue)
    {
        if (IsWithinGrid(indexNode, GridModel))
        {
            positionValue = new Vector3(indexNode.x, indexNode.y, 0) * GridModel.CellSize + (Vector3)GridModel.PositionOrigin;
            return true;
        }

        positionValue = Vector3.negativeInfinity;
        return false;
    }

    public static bool TryGetPositionInGrid(Vector3 objectPosition, out Vector2Int positionValue)
    {
        if (!IsWithinGrid(ConvertingPosition(objectPosition), GridModel))
        {
            positionValue = Vector2Int.one * int.MinValue;
            return false;
        }

        int X = Mathf.FloorToInt((objectPosition - (Vector3)GridModel.PositionOrigin).x / GridModel.CellSize);
        int Y = Mathf.FloorToInt((objectPosition - (Vector3)GridModel.PositionOrigin).y / GridModel.CellSize);
        positionValue = new Vector2Int(X, Y);
        return true;
    }
    public static bool TryGetPositionInGrid<T>(Vector2Int indexNode, GridModel<T> grid, out Vector3 positionValue)
    {
        if (IsWithinGrid(indexNode, grid))
        {
            positionValue = new Vector3(indexNode.x, indexNode.y, 0) * grid.CellSize + (Vector3)grid.PositionOrigin;
            return true;
        }

        positionValue = Vector3.negativeInfinity;
        return false;
    }
    public static bool TryGetPositionInGrid<T>(Vector3 objectPosition, GridModel<T> grid, out Vector2Int positionValue)
    {
        if (!IsWithinGrid(ConvertingPosition(objectPosition), grid))
        {
            positionValue = Vector2Int.up * int.MinValue;
            return false;
        }

        int X = Mathf.FloorToInt((objectPosition - (Vector3)grid.PositionOrigin).x / grid.CellSize);
        int Y = Mathf.FloorToInt((objectPosition - (Vector3)grid.PositionOrigin).y / grid.CellSize);
        positionValue = new Vector2Int(X, Y);
        return true;
    }

}

using UnityEngine;
public class GridUtility
{

    public static T GetNodeByIndex<T>(Vector2Int index, T[,] grid)
    {
        return grid[index.x, index.y];
    }

    public static bool IsWithinGrid<T>(Vector2Int indexNode, T[,] grid)
    {
        return indexNode.x >= 0 && indexNode.x < grid.GetLength(0) && indexNode.y >= 0 && indexNode.y < grid.GetLength(1);
    }

}

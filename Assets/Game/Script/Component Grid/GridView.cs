using System;
using UnityEditor;
using UnityEngine;

public class GridView : ModelView
{
    private Vector2Int Size => GameData<Main>.Boot.GridController.Grid.Size;
    private float CellSize => GameData<Main>.Boot.GridController.Grid.CellSize;
    private GridController GridController => GameData<Main>.Boot.GridController;

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (GameData<Main>.Boot == null || GameData<Main>.Boot.GridController == null)
            return;
        
        Gizmos.color = Color.red;

        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.DrawLine(GridUtility.ConvertingPosition(new Vector2(x, y)), GridUtility.ConvertingPosition(new Vector2(x, y + 1)));
                Gizmos.DrawLine(GridUtility.ConvertingPosition(new Vector2(x, y)), GridUtility.ConvertingPosition(new Vector2(x + 1, y)));
            }
            Gizmos.DrawLine(GridUtility.ConvertingPosition(new Vector2(0, Size.y)), GridUtility.ConvertingPosition(new Vector2(Size.x, Size.y)));
            Gizmos.DrawLine(GridUtility.ConvertingPosition(new Vector2(Size.x, 0)), GridUtility.ConvertingPosition(new Vector2(Size.x, Size.y)));
        }
    }
      #endif
}

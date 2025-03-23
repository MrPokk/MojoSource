using System;
using UnityEditor;
using UnityEngine;

public class GridView : ModelView<GridView>
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
                Gizmos.DrawLine(GridController.Grid.GetWorldPose(new(x, y)), GridController.Grid.GetWorldPose(new(x, y + 1)));
                Gizmos.DrawLine(GridController.Grid.GetWorldPose(new(x, y)), GridController.Grid.GetWorldPose(new(x + 1, y)));
            }
            Gizmos.DrawLine(GridController.Grid.GetWorldPose(new(0, Size.y)), GridController.Grid.GetWorldPose(new(Size.x, Size.y)));
            Gizmos.DrawLine(GridController.Grid.GetWorldPose(new(Size.x, 0)), GridController.Grid.GetWorldPose(new(Size.x, Size.y)));
        }
    }
      #endif
}

using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;
public class GridController
{
    public GridModel<GridNode> Grid { get; private set; }
    private GridView _gridView;
    
    public GridController(Vector2Int gridSize, float cellSize, GridView gridView)
    {
        _gridView = gridView;

        Grid = new GridModel<GridNode>(gridSize, cellSize, _gridView.gameObject.transform.position);
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                
                Grid.SetValue(gridPosition,new GridNode(gridPosition));
                _gridView.Init(ref x, ref y, ref cellSize);
            }
        }
        
    }

}

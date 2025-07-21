using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;
public class GridController
{
    public Vector2Int GridMousePosition {
        get {
            var IsGrid = TryGetPositionInGrid(MouseInteraction.MousePose, out var mousePosition);
            if (IsGrid)
                return mousePosition;

            return Vector2Int.one * int.MinValue;
        }
    }
    private readonly GridModel _grid;
    private readonly GridView _gridView;

    public GridController(Vector2Int gridSize, float cellSize, GridView gridView)
    {
        _grid = new GridModel(gridSize, cellSize, gridView.gameObject.transform.position);
        _gridView = gridView;
        _gridView.Init(gridSize,this,cellSize);
    }

    public float GetCellSize()
    {
        return _grid.CellSize;
    }
    
    public GridNode[,] GetArray()
    {
        return _grid.Array;
    }


    public bool TryGetPositionInGrid(Vector2Int indexNode, out Vector3 positionValue)
    {
        if (IsWithinGrid(indexNode))
        {
            positionValue = new Vector3(indexNode.x, indexNode.y, 0) * _grid.CellSize + (Vector3)_grid.PositionOrigin;
            return true;
        }

        positionValue = Vector3.negativeInfinity;
        return false;
    }

    public bool TryGetPositionInGrid(Vector3 objectPosition, out Vector2Int positionValue)
    {
        if (!IsWithinGrid(ConvertingPosition(objectPosition)))
        {
            positionValue = Vector2Int.one * int.MinValue;
            return false;
        }

        int X = Mathf.FloorToInt((objectPosition - (Vector3)_grid.PositionOrigin).x / _grid.CellSize);
        int Y = Mathf.FloorToInt((objectPosition - (Vector3)_grid.PositionOrigin).y / _grid.CellSize);
        positionValue = new Vector2Int(X, Y);
        return true;
    }




    public void SetTypeInGrid(Vector2Int index, GridNode.TypeNode value)
    {
        if (IsWithinGrid(index))
        {
            _grid.Array[index.x, index.y].Type = value;
        }
    }
    
    public void SetTypeInGrid(Vector3 worldPosition, GridNode.TypeNode value)
    {
       var indexGrid= ConvertingPosition(worldPosition);
        if (IsWithinGrid(indexGrid))
        {
            _grid.Array[indexGrid.x, indexGrid.y].Type = value;
        }
    }
    public void SetValueInGrid(Vector2Int index, GridNode value)
    {
        if (IsWithinGrid(index))
        {
            _grid.Array[index.x, index.y] = value;
        }
    }

    public GridNode GetNodeByIndex(Vector2Int index)
    {
        return _grid.Array[index.x, index.y];
    }

    public Vector3 ConvertingPosition(Vector2Int index)
    {
        return new Vector3(index.x, index.y, 0) * _grid.CellSize + (Vector3)_grid.PositionOrigin;
    }
    public Vector2Int ConvertingPosition(Vector3 worldPose)
    {
        int X = Mathf.FloorToInt((worldPose - (Vector3)_grid.PositionOrigin).x / _grid.CellSize);
        int Y = Mathf.FloorToInt((worldPose - (Vector3)_grid.PositionOrigin).y / _grid.CellSize);
        return new Vector2Int(X, Y);
    }
    public bool IsWithinGrid(Vector2Int indexNode)
    {
        return indexNode.x >= 0 && indexNode.x < _grid.Size.x && indexNode.y >= 0 && indexNode.y < _grid.Size.y;
    }

}

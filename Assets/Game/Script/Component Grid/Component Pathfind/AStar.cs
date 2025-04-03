using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AStar
{
    private HashSet<Vector2Int> _open;
    private HashSet<Vector2Int> _close;


    private Dictionary<Vector2Int, Vector2Int> _closed;

    private GridNode _current;

    private GridNode _endNode;
    private GridNode _startNode;

    private GridNode[,] _grid;

    private readonly static Vector2Int[] _neighborsOffset = new[]
    {
        Vector2Int.right,
        Vector2Int.left,

        Vector2Int.up,
        Vector2Int.down,

        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 1),
    };

    public static List<Vector2Int> TryGetPathFind(Vector2Int start, Vector2Int end, GridNode[,] grid)
    {
        if (grid == null)
        {
            Debug.LogError($"ERROR: {nameof(grid)}");
            return null;
        }

        if (!GridUtility.IsWithinGrid(start, grid) || !GridUtility.IsWithinGrid(end, grid))
        {
            Debug.LogWarning("WARNING: start or end are not within the grid");
            return null;
        }
        
        if (start == end)
        {
            Debug.LogWarning("WARNING: start or end are not within the grid");
            return null;
        }

        return new AStar().Find(start, end, grid);
    }

    private List<Vector2Int> Find(Vector2Int startNode, Vector2Int endNode, GridNode[,] grid)
    {
        _grid = grid.Clone() as GridNode[,];

        _open = new HashSet<Vector2Int>();
        _close = new HashSet<Vector2Int>();

        _startNode = _grid[startNode.x, startNode.y];
        _startNode.GCost = 0;
        _startNode.HCost = CalculateHCost(startNode, endNode);

        _grid[startNode.x, startNode.y] = _startNode;

        _open.Add(_startNode.Index);

        _endNode = grid[endNode.x, endNode.y];
        _grid[endNode.x, endNode.y] = _endNode;

        while (_open.Count > 0)
        {
            _current = GetLowerFCost(_open);
            _grid[_current.Index.x, _current.Index.y] = _current;

            _open.Remove(_current.Index);
            _close.Add(_current.Index);

            if (_current.Index == _endNode.Index)
            {
                return GetPath(_endNode.Index);
            }

            var Neighbors = GetIndexNeighbors(_current.Index);
            foreach (var Neighbor in Neighbors)
            {
                GridNode NeighborNode = _grid[Neighbor.x, Neighbor.y];

                int TentativeGCost = _current.GCost + CalculateHCost(_current.Index, Neighbor);
                if (TentativeGCost < NeighborNode.GCost)
                {
                    NeighborNode.IndexParent = _current.Index;
                    NeighborNode.GCost = TentativeGCost;
                    NeighborNode.HCost = CalculateHCost(NeighborNode.Index, _endNode.Index);

                    _grid[NeighborNode.Index.x, NeighborNode.Index.y] = NeighborNode;

                    _open.Add(NeighborNode.Index);
                }
            }
        }

        throw new Exception("END PATH FIND ERROR");
    }

    private int CalculateHCost(Vector2Int startNode, Vector2Int endNode)
    {
        return (int)Vector2Int.Distance(startNode, endNode);
    }

    private List<Vector2Int> GetPath(Vector2Int endNodeIndex)
    {
        List<Vector2Int> Path = new List<Vector2Int>();

        GridNode CurrentNode = GridUtility.GetNodeByIndex(endNodeIndex, _grid);
        Path.Add(CurrentNode.Index);

        while (CurrentNode.IndexParent != Vector2Int.one * -1)
        {
            GridNode CameFromNode = GridUtility.GetNodeByIndex(CurrentNode.IndexParent, _grid);
            Path.Add(CameFromNode.Index);
            CurrentNode = CameFromNode;
        }

        Path.Reverse();

        return Path;
    }

    private List<Vector2Int> GetIndexNeighbors(Vector2Int currentNodeIndex)
    {
        var NeighborAll = new List<Vector2Int>();

        foreach (Vector2Int NeighborsOffset in _neighborsOffset)
        {
            var NeighborIndex = currentNodeIndex + NeighborsOffset;

            if (GridUtility.IsWithinGrid(NeighborIndex, _grid) && IsNodeWalkable(NeighborIndex))
                NeighborAll.Add(NeighborIndex);
        }

        return NeighborAll;
    }

    private bool IsNodeWalkable(Vector2Int indexNode)
    {
        //TODO: Добавить проверку
        return true;
    }


    //TODO: Возможно SorterSet Что бы сразу в список доступных нод добавлять ноды по цене FCost
    private GridNode GetLowerFCost(HashSet<Vector2Int> nodeArray)
    {
        var IndexLowerNode = GridUtility.GetNodeByIndex(nodeArray.First(), _grid);
        foreach (var NodeIndex in nodeArray)
        {
            var NodeElement = GridUtility.GetNodeByIndex(NodeIndex, _grid);

            if (NodeElement.FCost < IndexLowerNode.FCost)
                IndexLowerNode = NodeElement;
        }

        return IndexLowerNode;
    }

}

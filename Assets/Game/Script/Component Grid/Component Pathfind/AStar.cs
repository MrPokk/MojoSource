using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {
        private HashSet<Vector2Int> _open;
        private HashSet<Vector2Int> _close;
        
        private GridNode _current;

        private GridNode _endNode;
        private GridNode _startNode;

        private GridNode[,] _grid;

        private readonly static Vector2Int[] _neighborsOffset = 
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



        public static List<Vector2Int> TryGetPathFind(Vector2Int start, Vector2Int end)
        {
            return ValidatePath(start, end) ? new AStar().Find(start, end, GridUtility.GridModel.Array) : null;
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

                var neighbors = GetIndexNeighbors(_current.Index);
                foreach (var neighbor in neighbors)
                {

                    GridNode neighborNode = _grid[neighbor.x, neighbor.y];

                    int tentativeGCost = _current.GCost + CalculateHCost(_current.Index, neighbor);
                    if (tentativeGCost < neighborNode.GCost)
                    {
                        neighborNode.IndexParent = _current.Index;
                        neighborNode.GCost = tentativeGCost;
                        neighborNode.HCost = CalculateHCost(neighborNode.Index, _endNode.Index);

                        _grid[neighborNode.Index.x, neighborNode.Index.y] = neighborNode;

                        _open.Add(neighborNode.Index);
                    }
                }
            }

            return null;
        }

        private int CalculateHCost(Vector2Int startNode, Vector2Int endNode)
        {
            return (int)Vector2Int.Distance(startNode, endNode);
        }

        private List<Vector2Int> GetPath(Vector2Int endNodeIndex)
        {
            var path = new List<Vector2Int>();

            var currentNode = GridUtility.GetNodeByIndex(endNodeIndex, _grid);
            path.Add(currentNode.Index);

            while (currentNode.IndexParent != Vector2Int.one * -1)
            {
                var cameFromNode = GridUtility.GetNodeByIndex(currentNode.IndexParent, _grid);
                path.Add(cameFromNode.Index);
                currentNode = cameFromNode;
            }

            path.Reverse();

            return path;
        }

        private List<Vector2Int> GetIndexNeighbors(Vector2Int currentNodeIndex)
        {
            var neighborAll = new List<Vector2Int>();

            foreach (var neighborsOffset in _neighborsOffset)
            {
                var neighborIndex = currentNodeIndex + neighborsOffset;

                if (GridUtility.IsWithinGrid(neighborIndex, _grid) && IsNodeWalkable(neighborIndex, _grid))
                    neighborAll.Add(neighborIndex);
            }

            return neighborAll;
        }

        private bool IsNodeWalkable(Vector2Int indexNode, GridNode[,] grid)
        {
            var gridNode = GridUtility.GetNodeByIndex(indexNode, grid);
            if (gridNode.Type == GridNode.TypeNode.SimplyNode)
            {
                return true;
            }
            else if (gridNode.Type == GridNode.TypeNode.Wall)
            {
                return false;
            }
            return false;
        }

        private GridNode GetLowerFCost(HashSet<Vector2Int> nodeArray)
        {
            var indexLowerNode = GridUtility.GetNodeByIndex(nodeArray.First(), _grid);
            foreach (var nodeIndex in nodeArray)
            {
                var nodeElement = GridUtility.GetNodeByIndex(nodeIndex, _grid);

                if (nodeElement.FCost < indexLowerNode.FCost)
                    indexLowerNode = nodeElement;
            }

            return indexLowerNode;
        }

    }
}

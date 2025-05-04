using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {

        public static List<Vector2Int> TryGetPathFindNearest(Vector2Int start, Vector2Int end, out Vector2Int nearestNode)
        {
            var aStart = new AStar();
            var nearestNeighbor = aStart.GetNearestIndexNeighbors(end);
            var endPosition = GetNearestIndexNode(start, nearestNeighbor);

            nearestNode = endPosition;
            return ValidatePath(start, end) ? aStart.Find(start, endPosition, GridUtility.GridModel.Array) : null;
        }
        private List<Vector2Int> GetNearestIndexNeighbors(Vector2Int currentNodeIndex)
        {
            var neighborAll = new List<Vector2Int>();
            foreach (Vector2Int neighborsOffset in _neighborsOffset)
            {
                var neighborIndex = currentNodeIndex + neighborsOffset;

                if (GridUtility.IsWithinGrid(neighborIndex) && IsNodeWalkable(neighborIndex, GridUtility.GridModel.Array))
                    neighborAll.Add(neighborIndex);
            }

            return neighborAll;
        } 

        private static Vector2Int GetNearestIndexNode(Vector2Int start, List<Vector2Int> nearestNeighbors)
        {
            var aStart = new AStar();
            var hCostFirst = aStart.CalculateHCost(start, nearestNeighbors.First());
            var nearestPosition = nearestNeighbors.First();
            
            foreach (var nearest in nearestNeighbors)
            {
                var nearestHCost = aStart.CalculateHCost(start, nearest);
                if (nearestHCost >= hCostFirst)
                    continue;
                
                hCostFirst = nearestHCost;
                nearestPosition = nearest;
            }
            return nearestPosition;
        }

    }
}

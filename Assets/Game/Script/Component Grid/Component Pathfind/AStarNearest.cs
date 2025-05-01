using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {

        public static List<Vector2Int> TryGetPathFindNearest(Vector2Int start, Vector2Int end, out Vector2Int nearestNode)
        {
            var AStart = new AStar();
            var NearestNeighbords = AStart.GetNearestIndexNeighbors(end);
            var endPosition = GetNearestIndexNode(start, NearestNeighbords);

            nearestNode = endPosition;
            return ValidatePath(start, end) ? AStart.Find(start, endPosition, GridUtility.GridModel.Array) : null;
        }
        private List<Vector2Int> GetNearestIndexNeighbors(Vector2Int currentNodeIndex)
        {
            var NeighborAll = new List<Vector2Int>();
            foreach (Vector2Int NeighborsOffset in _neighborsOffset)
            {
                var NeighborIndex = currentNodeIndex + NeighborsOffset;

                if (GridUtility.IsWithinGrid(NeighborIndex) && IsNodeWalkable(NeighborIndex, GridUtility.GridModel.Array))
                    NeighborAll.Add(NeighborIndex);
            }

            return NeighborAll;
        } 

        private static Vector2Int GetNearestIndexNode(Vector2Int start, List<Vector2Int> nearestNeighbors)
        {
            var AStart = new AStar();
            var HCostFirst = AStart.CalculateHCost(start, nearestNeighbors.First());
            Vector2Int NearestPosition = nearestNeighbors.First();

            foreach (var Nearest in nearestNeighbors)
            {
                var NearestHCost = AStart.CalculateHCost(start, Nearest);
                if (NearestHCost >= HCostFirst)
                    continue;

                HCostFirst = NearestHCost;
                NearestPosition = Nearest;
            }
            return NearestPosition;
        }

    }
}

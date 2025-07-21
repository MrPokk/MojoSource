using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {
        public static List<Vector2Int> TryGetPathFindNearest(
            GridNode[,] grid,
            Vector2Int start,
            Vector2Int end,
            in Vector2Int[] allNeighborOffsets,
            out Vector2Int nearestReachableNode,
            Predicate<Vector2Int> nodeCondition = null
        )
        {
            var pathfinder = new AStar();

            var validNeighbors = pathfinder.GetValidNeighbors(
                end,
                grid,
                allNeighborOffsets,
                nodeCondition);

            if (validNeighbors.Count == 0)
            {
                nearestReachableNode = Vector2Int.up * -1;
                return null;
            }

            var isNearestReachableNode = pathfinder.FindNearestToStart(start, validNeighbors);
            if (isNearestReachableNode != null)
            {
                nearestReachableNode = isNearestReachableNode.Value;
                return ValidatePath(start, nearestReachableNode, grid)
                    ? pathfinder.Find(start, nearestReachableNode, grid, allNeighborOffsets)
                    : null;
            }
            
            nearestReachableNode = Vector2Int.up * -1;
            return null;
        }

        private List<Vector2Int> GetValidNeighbors(
            Vector2Int centerPosition,
            in GridNode[,] grid,
            in Vector2Int[] neighborOffsets,
            Predicate<Vector2Int> condition
        )
        {
            var validNodes = new List<Vector2Int>();

            if (IsWithinGrid(centerPosition, grid) && IsNodeWalkable(centerPosition, grid) &&
                (condition == null || condition(centerPosition)))
            {
                validNodes.Add(centerPosition);
            }

            foreach (var offset in neighborOffsets)
            {
                var neighborPosition = centerPosition + offset;

                if (!IsWithinGrid(neighborPosition, grid) || !IsNodeWalkable(neighborPosition, grid))
                    continue;

                if (condition != null && !condition(neighborPosition))
                    continue;

                validNodes.Add(neighborPosition);
            }

            return validNodes;
        }

        private Vector2Int? FindNearestToStart(Vector2Int start, List<Vector2Int> potentialNodes)
        {
            if (potentialNodes.Count == 0)
                return null;

            return potentialNodes
                .OrderBy(node => CalculateHCost(start, node))
                .First();
        }
    }
}

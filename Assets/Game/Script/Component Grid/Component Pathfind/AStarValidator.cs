using UnityEngine;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {
        private static bool ValidatePath(Vector2Int start, Vector2Int end, GridNode[,] gridNodes)
        {
            if (gridNodes == null || start == end)
                return false;

            if (!IsWithinGrid(start, gridNodes) || !IsWithinGrid(end, gridNodes))
                return false;

            return true;
        }
    }
}

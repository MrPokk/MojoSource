using UnityEngine;

namespace Game.Script.Component_Grid.Component_Pathfind
{
    public partial class AStar
    {
        private static bool ValidatePath(Vector2Int start, Vector2Int end)
        {
            if (GridUtility.GridModel == null || start == end)
                return false;

            if (!GridUtility.IsWithinGrid(start, GridUtility.GridModel.Array) || !GridUtility.IsWithinGrid(end, GridUtility.GridModel.Array))
                return false;
            
            return true;
        }
    }
}

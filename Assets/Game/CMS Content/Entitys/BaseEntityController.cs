using Game.Engine_Component.CMS;
using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;

namespace Game.CMS_Content.Entitys
{
    public class BaseEntityController : CMSManager
    {
        private Vector2Int _positionCurrent;

        public void SpawnEntityInGrid<T>(Vector2Int positionGrid) where T : BaseEntityModel, new()
        {
            _positionCurrent = positionGrid;

            GridUtility.SetTypeInGrid(positionGrid, GridNode.TypeNode.Wall);

            SpawnEntity<T>();
        }

        protected override void SpawnEntity<T>()
        {
            Create<T>(out var entity);

            var inGrid = GridUtility.TryGetPositionInGrid(_positionCurrent, out Vector3 positionInGrid);
            if (!inGrid)
                return;
            var cellSize = GameData<Main>.Boot.GridController.Grid.CellSize;

            entity.transform.localScale = new Vector3(cellSize, cellSize, 0);
            
            entity.transform.position = positionInGrid + (new Vector3(cellSize,cellSize)/2);
        }
    }
}

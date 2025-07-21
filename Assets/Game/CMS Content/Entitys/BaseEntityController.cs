using Game.Engine_Component.CMS;
using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;

namespace Game.CMS_Content.Entitys
{
    public sealed class BaseEntityController : CMSManager
    {
        private Vector2Int _positionCurrent;

        public void SpawnEntityInGrid<T>(Vector2Int positionGrid) where T : BaseEntityModel, new()
        {
            _positionCurrent = positionGrid;
            SpawnEntity<T>();
        }

        protected override void SpawnEntity<T>()
        {
            Create<T>(out var entity);
            var gridController = GameData<Main>.Boot.GetGridController(CMS.Get<T>());
            gridController.SetTypeInGrid(_positionCurrent, GridNode.TypeNode.Wall);

            var inGrid = gridController.TryGetPositionInGrid(_positionCurrent, out Vector3 positionInGrid);
            if (!inGrid)
                return;
            var cellSize = gridController.GetCellSize();

            entity.transform.localScale = new Vector3(cellSize, cellSize, 0);

            entity.transform.position = positionInGrid + (new Vector3(cellSize, cellSize) / 2);
        }
    }
}

using UnityEngine;

namespace Game.CMS_Content.Entity
{
    public class BaseEntityController : CMSManager
    {

        private Vector2Int _positionCurrent;

        public void SpawnEntityInGrid<T>(Vector2Int positionGrid) where T : BaseEntityModel, new()
        {
            _positionCurrent = positionGrid;

            SpawnEntity<T>();
        }

        protected override void SpawnEntity<T>()
        {
            Create<T>(out var Entity);

            var PositionInGrid = GridUtility.TryGetPositionInGrid(_positionCurrent, GameData<Main>.Boot.GridController.Grid);
            PositionInGrid += (Entity.transform.localScale / 2);

            if (PositionInGrid != null)
                Entity.transform.position = (Vector3)PositionInGrid;
        }
    }
}

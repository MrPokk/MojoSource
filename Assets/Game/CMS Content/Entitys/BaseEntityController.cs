using Game.CMS_Content.Entity;
using UnityEngine;

namespace Game.CMS_Content.Entitys
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

            var PositionInGrid = GridUtility.TryGetPositionInGrid(_positionCurrent, out Vector3 positionInGrid);
            if (PositionInGrid)
            {
                positionInGrid += (Entity.transform.localScale / 2);

                if (PositionInGrid != null)
                    Entity.transform.position = positionInGrid;
            }
        }
    }
}

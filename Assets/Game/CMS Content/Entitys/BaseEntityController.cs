using Game.CMS_Content.Entity;
using Game.Engine_Component.CMS;
using Game.Script.Component_Grid.Component_Pathfind;
using System.Collections.Generic;
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

            positionInGrid += (entity.transform.localScale / 2);
            entity.transform.position = positionInGrid;
        }
    }
}

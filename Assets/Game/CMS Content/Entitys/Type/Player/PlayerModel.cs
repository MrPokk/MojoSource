using Game.CMS_Content.Entity.Type;
using Game.Script.Component_Grid.Component_Pathfind;
using Game.Script.ECS.Global_Components;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Player
{
    public class PlayerModel : BaseEntityModel
    {
        public PlayerModel()
        {
            Define<RaycastingComponent>(out var raycastCommand);

            Components.View.LoadView<PlayerView>(PathResources.ENTITY);

            Components.Health.Init(20);
            Components.Move.Init(2, MoveTo);

        }


        private void MoveTo(Vector2Int positionTo)
        {
            var isStartPosition = GridUtility.TryGetPositionInGrid(ViewObject.transform.position, out var startPosition);
            var isEndPosition = GridUtility.IsWithinGrid(positionTo);

            if (!isStartPosition || !isEndPosition)
                return;

            var path = AStar.TryGetPathFind(startPosition, positionTo);

            Components.Move.MakeByStep(path, startPosition, positionTo, ViewObject);

        }
    }
}

using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.Script.Component_Grid.Component_Pathfind;
using Game.Script.ECS.Global_Components;
using System;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Player
{
    public class PlayerModel : BaseEntityModel, IPlayer
    {

        public PlayerModel()
        {
            Define<RaycastingComponent>(out var raycastCommand);

            Components.View.LoadView<PlayerView>(PathResources.ENTITY);

            Components.Health.Init(20);
            Components.Move.Init(
                moveMethod: MoveTo, 
                neighborsAll: new []
                {
                    Vector2Int.right,
                    Vector2Int.left,

                    Vector2Int.up,
                    Vector2Int.down,
                }, 
                maxCountStep: 1,
                gridController: GameData<Main>.Boot.GetGridController(this));
        }

        private void MoveTo(Vector2Int positionTo)
        {
            GetComponent<MoveComponent>(out var moveComponent);

            var isStartPosition = moveComponent.GridController.TryGetPositionInGrid(View.transform.position, out var startPosition);
            var isEndPosition = moveComponent.GridController.IsWithinGrid(positionTo);

            if (!isStartPosition || !isEndPosition)
                return;
            

            var path = AStar.TryGetPathFind(
                start: startPosition, 
                end: positionTo, 
                neighborsAll: moveComponent.NeighborsOffset, 
                grid: moveComponent.GridController.GetArray());

            Components.Move.MakeByStep(path, View);
        }
    }
}

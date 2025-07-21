using Game.CMS_Content.Cards;
using Game.CMS_Content.Cards.Type;
using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Component_Grid.Component_Pathfind;
using Game.Script.Utility.FromGame;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Enemys
{
    public class FrogModel : BaseEntityModel, IEnemy
    {

        public FrogModel()
        {
            Define<AttackComponent>(out var attackComponent);

            Components.View.LoadView<FrogView>(PathResources.ENTITY);
            Components.Health.Init(30);
            Components.Move.Init(
                moveMethod: MoveTo,
                neighborsAll: new[]
                {
                    new Vector2Int(-1, -1),
                    new Vector2Int(-1, 1),
                    new Vector2Int(1, -1),
                    new Vector2Int(1, 1),
                },
                maxCountStep: 2,
                gridController: GameData<Main>.Boot.GetGridController(this));


            attackComponent.Init(
                attackTo: AttackTo,
                card: new[]
                {
                    new DamageCard(),
                },
                attackZone: new[]
                {
                    new Vector2Int(-1, -1),
                    new Vector2Int(-1, 1),
                    new Vector2Int(1, -1),
                    new Vector2Int(1, 1),
                });
        }

        private void MoveTo(Vector2Int playerPosition)
        {
            GetComponent<MoveComponent>(out var moveComponent);

            var isStartPosition = moveComponent.GridController.TryGetPositionInGrid(View.transform.position, out var startPosition);
            var isEndPosition = moveComponent.GridController.IsWithinGrid(playerPosition);

            if (!isStartPosition || !isEndPosition)
                return;

            var path = AStar.TryGetPathFindNearest(
                start: startPosition,
                end: playerPosition,
                allNeighborOffsets: moveComponent.NeighborsOffset,
                grid: moveComponent.GridController.GetArray(),
                nearestReachableNode: out var nearestNode);

            Components.Move.MakeByStep(path, View);
        }

        private void AttackTo()
        {
            GetComponent<AttackComponent>(out var attack);

            GetNearestEntity<PlayerModel>(out var player);

            if (attack.CheckZoneAttack(this, player))
            {
                player.GetComponent<CardInsideComponent>(out var cardInsideComponent);
                cardInsideComponent.AddCard(attack.GetAllAttack());
            }
        }

        private void GetNearestEntity<T>(out CMSEntity nearestPlayer) where T : CMSEntity
        {
            var nearest = TransformUtility.FindToNearest<T>(GetView());
            if (nearest == null)
            {
                nearestPlayer = null;
                return;
            }

            nearest.GetComponent<MoveComponent>(out var gridNearest);
            nearestPlayer = nearest;
            gridNearest.GridController.ConvertingPosition(nearest.GetViewPosition3D());

        }
    }
}

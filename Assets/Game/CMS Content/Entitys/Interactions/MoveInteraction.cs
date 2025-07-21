using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Utility.FromGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    
    public class MoveInteraction : BaseInteraction, IEnterInNextTurn
    {
        public override Priority PriorityInteraction { get => Priority.High; }
        public void UpdateTurn()
        {
            var allEntities = CMS.Get<BaseEntityController>().GetEntities().Values;

            var enemies = new Dictionary<MoveComponent, CMSEntity>();

            foreach (var entity in allEntities)
            {
                entity.GetComponent<MoveComponent>(out var moveComponent);

                moveComponent.CountTurnUpdate();
                if (entity is IEnemy)
                    enemies.Add(moveComponent, entity);
            }

            GameData<Main>.Coroutine.Run(ProcessEnemyMoves(enemies));
        }

        private IEnumerator ProcessEnemyMoves(Dictionary<MoveComponent, CMSEntity> enemies)
        {
            foreach (var enemy in enemies)
            {
                MoveEnemy(enemy.Key, enemy.Value);
                yield return new WaitWhile(() => enemy.Key.IsWalking);
            }
        }

        private void MoveEnemy(MoveComponent moveComponent, CMSEntity entity)
        {
            var nearestPlayer = TransformUtility.FindToNearest<PlayerModel>(entity.GetView());
            if (nearestPlayer == null)
                return;
            
            var gridPlayer = GameData<Main>.Boot.GetGridController(nearestPlayer);
            var positionPlayer = nearestPlayer.GetViewPosition3D();
            
            gridPlayer.TryGetPositionInGrid(positionPlayer, out var positionInGrid);
            moveComponent.MoveMethod?.Invoke(positionInGrid);
        }
    }


}

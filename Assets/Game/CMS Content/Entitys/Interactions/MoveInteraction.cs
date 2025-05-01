using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class MoveInteraction : BaseInteraction, IEnterInNextTurn
    {
        public void UpdateTurn()
        {
            IEnumerable<CMSEntity> allEntities = CMS.Get<BaseEntityController>().GetEntities().Values;

            Dictionary<MoveComponent, CMSEntity> enemies = new Dictionary<MoveComponent, CMSEntity>();

            foreach (var entity in allEntities)
            {
                entity.GetComponent<MoveComponent>(out var moveComponent);
                moveComponent.CountTurnUpdate();

                if (entity is IEnemy)
                    enemies.Add(moveComponent,entity);
            }

            GameData<Main>.Coroutine.Run(ProcessEnemyMoves(enemies));
        }

        private IEnumerator ProcessEnemyMoves(Dictionary<MoveComponent, CMSEntity>enemies)
        {
            yield return new WaitForSeconds(1f);
            
            foreach (var enemy in enemies)
            {
                MoveEnemy(enemy.Key,enemy.Value);
                yield return new WaitWhile(() => enemy.Key.IsWalking);
            }
        }

        private void MoveEnemy(MoveComponent moveComponent, CMSEntity entity)
        {
            var nearestPlayer = TransformUtility.FindToNearest<PlayerModel>(entity.GetView());
            var positionPlayer = nearestPlayer.GetViewPosition2D();
            GridUtility.TryGetPositionInGrid(positionPlayer, out var positionInGrid);
            moveComponent.MoveMethod?.Invoke(positionInGrid);
        }
    }


}

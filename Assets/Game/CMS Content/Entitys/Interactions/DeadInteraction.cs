using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using Game.Script.Component_Grid.Component_Pathfind;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class DeadInteraction : BaseInteraction, IEnterInNextTurn, IEnterInDead, IEnterInAttack
    {
        public override Priority PriorityInteraction { get => Priority.High; }

        public void UpdateTurn()
        {
            UpdateDead();
        }
        public void UpdateAttack()
        {
            UpdateDead();
        }

        public void UpdateDead()
        {
            var allEntity = CMS.Get<BaseEntityController>().GetEntities();
            var dealEntity = new List<ModelView>();

            foreach (var entity in allEntity.Values)
            {
                entity.GetComponent<HealthComponent>(out var healthComponent);
                if (healthComponent.HealthCurrent <= 0)
                    dealEntity.Add(entity.GetView());
            }
            foreach (var entity in dealEntity)
            {
                ResetGrid(entity);

                CMS.Get<BaseEntityController>().DestroyEntity(entity);
            }
        }
        private void ResetGrid(ModelView entity)
        {
            entity.GetModel().GetComponent<MoveComponent>(out var moveComponent);
            var position = moveComponent.GridController.ConvertingPosition(entity.transform.position);
            moveComponent.GridController.SetTypeInGrid(position, GridNode.TypeNode.SimplyNode);
        }
    }
}

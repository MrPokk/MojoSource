using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class DeadInteraction : BaseInteraction, IEnterInNextTurn, IEnterInDead, IEnterInAttack
    {
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
                CMS.Get<BaseEntityController>().DestroyEntity(entity);
            }
        }
     
    }
}

using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using UnityEngine;

namespace Game.CMS_Content.Cards.Type
{
    public class ArsonCard : BaseCardModel
    {
        public ArsonCard()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadView<BaseCardView>(PathResources.CARD);

            Components.PriorityCardComponent.Priority = Priority.High;
            Components.ActionCardComponent.AbilityCard = AbilityCard;
        }
        private void AbilityCard(CMSEntity sourceEntity)
        {
            sourceEntity.GetComponent<HealthComponent>(out var healthComponent);
            healthComponent.Decrease(5);
        }
     
    }
}

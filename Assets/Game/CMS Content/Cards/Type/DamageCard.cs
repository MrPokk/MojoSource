using Engin.Utility;
using Game.CMS_Content.Cards.Components;
using Game.CMS_Content.Entitys.Components;
using System;
using UnityEngine;

namespace Game.CMS_Content.Cards.Type
{
    public class DamageCard : BaseCardModel
    {
        public DamageCard()
        {
            Define(out MultiplierComponent multilineComponent);
            Define(out ViewComponent viewComponent);
            viewComponent.LoadView<BaseCardView>(PathResources.CARD);
            multilineComponent.Init(2);

            Components.PriorityCardComponent.Init(Priority.Medium  );
            Components.ActionCardComponent.Init(AbilityCard);
        }

        private void AbilityCard(CMSEntity sourceEntity)
        {
            sourceEntity.GetComponent<HealthComponent>(out var healthComponent);
            GetComponent<MultiplierComponent>(out var multiplierComponent);
            var factor = multiplierComponent.UsingMultiplier();

            healthComponent.Decrease(5 * factor);
        }

    }
}

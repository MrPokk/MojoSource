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
            multilineComponent.Init(2);

            Components.View.LoadView<BaseCardView>(PathResources.CARD_BASE);
            Components.Priority.Init(Priority.Medium  );
            Components.Action.Init(AbilityCard);
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

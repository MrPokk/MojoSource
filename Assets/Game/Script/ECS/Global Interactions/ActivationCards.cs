using Engin.Utility;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entitys;
using Game.CMS_Content.Entitys.Components;
using UnityEngine;

namespace Game.Script.ECS.Global_Interactions
{
    public class ActivationCards : BaseInteraction, IEnterInNextTurn
    {
        public override Priority PriorityInteraction { get => Priority.High; }
        public void UpdateTurn()
        {
            var allEntity = CMS.Get<BaseEntityController>().GetEntities();
            foreach (var entity in allEntity.Values)
            {
                entity.GetComponent<CardInsideComponent>(out var cardInsideComponent);
                var allCards = cardInsideComponent.GetAllCard();
                foreach (BaseCardModel card in allCards)
                {
                    card.Components.ActionCardComponent.AbilityCard?.Invoke(entity);
                }
                cardInsideComponent.RemoveAllCard();
            }
        }
    }
}

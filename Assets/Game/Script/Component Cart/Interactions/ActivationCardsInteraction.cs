using Engin.Utility;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entitys;
using Game.CMS_Content.Entitys.Components;

namespace Game.Script.Component_Cart.Interactions
{
    public class ActivationCardsInteraction : BaseInteraction, IEnterInNextTurn
    {
        public override Priority PriorityInteraction { get => Priority.FIRST_TASK; }
        public void UpdateTurn()
        {
            ActivateCards();
        }

        public static void ActivateCards()
        {
            var allEntity = CMS.Get<BaseEntityController>().GetEntities();
            foreach (var entity in allEntity.Values)
            {
                entity.GetComponent<CardInsideComponent>(out var cardInsideComponent);
                var allCards = cardInsideComponent.GetAllCard();
                foreach (var card in allCards)
                {
                    card.Components.ActionCardComponent.AbilityCard?.Invoke(entity);
                }
                cardInsideComponent.RemoveAllCard();
            }
        }
    }
}

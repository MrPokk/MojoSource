using Engin.Utility;
using Game.CMS_Content.Cards.Components;
using Game.CMS_Content.Entitys.Components;


namespace Game.CMS_Content.Cards.Type
{
    public class MultiplierCard : BaseCardModel
    {
        public MultiplierCard()
        {
            Components.View.LoadView<BaseCardView>(PathResources.CARD_BASE);
            Components.Priority.Init(Priority.High);
            Components.Action.Init(AbilityCard);
        }

        private void AbilityCard(CMSEntity sourceEntity)
        {
            sourceEntity.GetComponent<CardInsideComponent>(out var cardInsideComponent);
            var cardsMultiplier = cardInsideComponent.GetAllCard();
            foreach (var card in cardsMultiplier)
            {
                card.GetComponent<MultiplierComponent>(out var multiplierComponent);
                multiplierComponent?.SetMultiplier(2);
            }
        }

    }
}

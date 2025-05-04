using Engin.Utility;
using Game.CMS_Content.Cards.Components;
using Game.CMS_Content.Entitys.Components;


namespace Game.CMS_Content.Cards.Type
{
    public class MultiplierCard : BaseCardModel
    {
        public MultiplierCard()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadView<BaseCardView>(PathResources.CARD);

            Components.PriorityCardComponent.Init(Priority.High);
            Components.ActionCardComponent.Init(AbilityCard);
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

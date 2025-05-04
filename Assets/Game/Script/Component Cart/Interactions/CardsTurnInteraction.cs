using Game.CMS_Content.Cards;
using Game.CMS_Content.Cards.Type;
using System;
using Random = UnityEngine.Random;

namespace Game.Script.Component_Cart.Interactions
{
    public class CardsTurnInteraction : BaseInteraction, IEnterInNextTurn
    {
        private int CountHand => GameData<Main>.Boot.HandCards.MaxCard;
        public void UpdateTurn()
        {
            AddNewCards();
        }
        private void AddNewCards()
        {
            var modelCards = CMS.GetAll<BaseCardModel>();
            var managerCards = CMS.Get<BaseCardController>();
            for (int i = 0; i < CountHand; i++)
            {
                var indexCard = Random.Range(0, modelCards.Count);
                var baseCardModel = modelCards[indexCard];
                
                managerCards.GiveCardInHand(baseCardModel);
            }
        }
    }
}

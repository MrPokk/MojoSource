using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CMS_Content.Card
{
    public class BaseCardController
    {

        private List<BaseCardModel> _loadedCards = new List<BaseCardModel>();

        public void GiveCardInHand<T>() where T : BaseCardModel, new()
        {
            GameData<Main>.Boot.HandCards.Add(InitViewModel(Create<T>()));
        }

        private T Create<T>() where T : BaseCardModel, new()
        {
            var ContentRegister = CMS.TryAddValue<T>();

            _loadedCards.Add(ContentRegister);
            return ContentRegister;
        }

        private BaseCardView InitViewModel(BaseCardModel cardModel)
        {
            if (cardModel.Components.FirstOrDefault(c => c.ID == typeof(ViewComponent)) is not ViewComponent viewInCard)
                throw new NullReferenceException("The card doesn't have ViewComponent");

            return GameData<Main>.Boot.InstantiateCMSEntity(viewInCard).GetComponent<BaseCardView>();
        }
    }
}

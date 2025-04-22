using Engin.Utility;
using Game.CMS_Content.Cards;
using System.Collections.Generic;

namespace Game.CMS_Content.Entitys.Components
{
    public class CardInsideComponent : IComponent
    {

        private List<BaseCardModel> InsideCard = new List<BaseCardModel>();
        
        public IReadOnlyList<BaseCardModel> GetAllCard<T>() where T : BaseCardModel
        {
            return InsideCard.FindAll(card => card is T);
        }
        public IReadOnlyList<BaseCardModel> GetAllCard()
        {
            return InsideCard;
        }
        public void AddCard(ModelView draggableObject)
        {
            if (draggableObject.GetModel() is BaseCardModel cardModel)
                AddCard(cardModel);
        }

        public void AddCard(BaseCardModel cardModel)
        {
            InsideCard.Add(cardModel);
        }
    }
}

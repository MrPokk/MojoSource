using Engin.Utility;
using Game.CMS_Content.Cards;
using System.Collections.Generic;

namespace Game.CMS_Content.Entitys.Components
{
    public class CardInsideComponent : IComponent
    {

        private readonly List<BaseCardModel> _insideCard = new List<BaseCardModel>();
        
        public IReadOnlyList<BaseCardModel> GetAllCard<T>() where T : BaseCardModel
        {
            return _insideCard.FindAll(card => card is T);
        }
        public IReadOnlyList<BaseCardModel> GetAllCard()
        {
            return _insideCard;
        }
        public void AddCard(ModelView draggableObject)
        {
            if (draggableObject.GetModel() is BaseCardModel cardModel)
                AddCard(cardModel);
        }

        public void AddCard(BaseCardModel cardModel)
        {
            _insideCard.Add(cardModel);
        }
    }
}

using Engin.Utility;
using Game.CMS_Content.Cards;
using JetBrains.Annotations;
using System;
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

        public void RemoveAllCard()
        {
            _insideCard.Clear();
        }
        public void RemoveCard([NotNull] BaseCardModel cardModel)
        {
            if (cardModel == null || !_insideCard.Contains(cardModel))
                throw new ArgumentNullException(nameof(cardModel));
            _insideCard.Remove(cardModel);
        }
        public void AddCard(ModelView draggableObject)
        {
            if (draggableObject.GetModel() is BaseCardModel cardModel)
                AddCard(cardModel);
        }

        public void AddCard(BaseCardModel cardModel)
        {
            _insideCard.Add(cardModel);
            Sort();
        }
        public void AddCard(IEnumerable<BaseCardModel> allCardModels)
        {
            _insideCard.AddRange(allCardModels);
            Sort();
        }

        private void Sort()
        {
            _insideCard.Sort((card1, card2) =>
                (int)card1.Components.Priority.Priority -
                (int)card2.Components.Priority.Priority);
        }
    }
}

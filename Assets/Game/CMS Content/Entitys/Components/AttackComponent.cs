using Engin.Utility;
using Game.CMS_Content.Cards;
using Game.Script.Utility;
using Game.Script.Utility.FromGame;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class AttackComponent : IComponent
    {

        private List<BaseCardModel> _attackCard;

        public void Init(params BaseCardModel[] card)
        {
            if (_attackCard != null)
                return;
            
            _attackCard = new List<BaseCardModel>();
            _attackCard.AddRange(card);
        }
        public void AddCardToAttack<T>(CMSEntity fromEntity) where T : CMSEntity
        {
            var nearestPlayer = TransformUtility.FindToNearest<T>(fromEntity.GetView());
            if (nearestPlayer == null)
                return;

            nearestPlayer.GetComponent<CardInsideComponent>(out var cardInsideComponent);
            cardInsideComponent.AddCard(_attackCard);
        }

        public IReadOnlyCollection<BaseCardModel> GetAllAttack()
        {
            return _attackCard;
        }
    }
}

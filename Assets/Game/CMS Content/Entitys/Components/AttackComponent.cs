using Engin.Utility;
using Game.CMS_Content.Cards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class AttackComponent : IComponent
    {

        private List<BaseCardModel> _attackCard;
        public Action OnAttack { get; private set; }
        public Vector2Int[] AttackZone { get; private set; }


        public void Init(IEnumerable<BaseCardModel> card, Action attackTo, Vector2Int[] attackZone)
        {
            if (_attackCard != null)
                return;

            _attackCard = new List<BaseCardModel>();
            _attackCard.AddRange(card);

            AttackZone = attackZone;
            OnAttack ??= attackTo;
        }

        public IReadOnlyList<BaseCardModel> GetAllAttack()
        {
            return _attackCard;
        }


        public bool CheckZoneAttack(CMSEntity fromEntity, CMSEntity toEntity)
        {
            if (fromEntity == null || toEntity == null)
                return false;
            
            fromEntity.GetComponent<MoveComponent>(out var fromMoveComponent);
            toEntity.GetComponent<MoveComponent>(out var toMoveComponent);

            var fromPositionGrid = fromMoveComponent.GridController.ConvertingPosition(fromEntity.GetViewPosition3D());
            var toPositionGrid = toMoveComponent.GridController.ConvertingPosition(toEntity.GetViewPosition3D());

            if (fromPositionGrid == toPositionGrid)
                return true;
            
            foreach (var offset in AttackZone)
            {
                var attackPosition = fromPositionGrid + offset;
                if (attackPosition == toPositionGrid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

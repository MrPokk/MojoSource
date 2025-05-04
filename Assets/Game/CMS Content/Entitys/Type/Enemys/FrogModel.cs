using Game.CMS_Content.Cards;
using Game.CMS_Content.Cards.Type;
using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.Script.Component_Grid.Component_Pathfind;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Enemys
{
    public class FrogModel : BaseEntityModel, IEnemy
    {
        
        public FrogModel()
        {
            Define<AttackComponent>(out var attackComponent);
            
            Components.View.LoadView<FrogView>(PathResources.ENTITY);
            Components.Health.Init(10);
            Components.Move.Init(2, MoveTo);

            var damageCard = new DamageCard();
            attackComponent.Init(damageCard);

        }

        private void MoveTo(Vector2Int positionTo)
        {
            var isStartPosition = GridUtility.TryGetPositionInGrid(ViewObject.transform.position, out var startPosition);
            var isEndPosition = GridUtility.IsWithinGrid(positionTo);

            if (!isStartPosition || !isEndPosition)
                return;

            var path = AStar.TryGetPathFindNearest(startPosition, positionTo, out var endPosition);

            Components.Move.MakeByStep(path, startPosition, endPosition, ViewObject);
        }
  
    }
}

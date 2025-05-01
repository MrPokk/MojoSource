using Game.CMS_Content.Cards;
using Game.CMS_Content.Entitys.Components;
using Game.Script.Global_Interactions;
using Game.Script.Utility;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class CardAddInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {

        public void Start()
        {
            DraggableInteraction.DropObject += DoCheckIsCard;
        }
        public void Stop()
        {
            DraggableInteraction.DropObject -= DoCheckIsCard;
        }
        private void DoCheckIsCard(ModelView cardView)
        {
            if (!cardView || cardView.GetModel() is not BaseCardModel cardModel)
                return;
            
            var nearestEntity = TransformUtility.FindToNearest<CMSEntity,CardInsideComponent>(cardView);
            var nearestDistance = Vector2.Distance(cardView.transform.position, nearestEntity.GetViewPosition2D());
              
            if (nearestDistance <= 1f)
                AddCard(nearestEntity, cardModel);
        }

        private void AddCard(CMSEntity entityModel, BaseCardModel cardModel)
        {
            entityModel.GetComponent<CardInsideComponent>(out var cardInsideComponent);
            cardInsideComponent.AddCard(cardModel);
            
           CMS.Get<BaseCardController>().DestroyEntity(cardModel.GetView().gameObject);
        }

    }
}

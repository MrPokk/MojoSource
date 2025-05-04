using Game.Engine_Component.CMS;
using UnityEngine;

namespace Game.CMS_Content.Cards
{
    public class BaseCardController : CMSManager
    {

        private HandCards HandCards => GameData<Main>.Boot.HandCards;
        public void GiveCardInHand<T>() where T : BaseCardModel, new()
        {
            if (HandCards.GetCountCard() < HandCards.MaxCard)
                SpawnEntity<T>();
        }

        public override void DestroyEntity(in GameObject ID)
        {
            HandCards.Remove(ID.GetComponent<BaseCardView>());
            base.DestroyEntity(in ID);
        }

        protected override void SpawnEntity<T>()
        {
            Create<T>(out var id);

            var Entity = GetEntityByID<BaseCardModel>(id);

            Entity.GetComponent<ViewComponent>(out var viewComponent);
            HandCards.Add(viewComponent.ViewModel as BaseCardView);
        }
    }
}

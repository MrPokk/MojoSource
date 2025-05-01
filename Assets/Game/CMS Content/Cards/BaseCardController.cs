using Game.CMS_Content.Entity;
using Game.Engine_Component.CMS;
using UnityEngine;

namespace Game.CMS_Content.Cards
{
    public class BaseCardController : CMSManager
    {
        public void GiveCardInHand<T>() where T : BaseCardModel, new()
        {
            SpawnEntity<T>();
        }

        public override void DestroyEntity(in GameObject ID)
        {
            GameData<Main>.Boot.HandCards.Remove(ID.GetComponent<BaseCardView>());
            base.DestroyEntity(in ID);
        }

        protected override void SpawnEntity<T>()
        {
            Create<T>(out var id);

            var Entity = GetEntityByID<BaseCardModel>(id);
            
            Entity.GetComponent<ViewComponent>(out var viewComponent);
            GameData<Main>.Boot.HandCards.Add(viewComponent.ViewModel as BaseCardView);
        }
    }
}

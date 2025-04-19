using Game.CMS_Content.Entity;
using UnityEngine;

namespace Game.CMS_Content.Cards
{
    public class BaseCardController : CMSManager
    {
        public void GiveCardInHand<T>() where T : BaseCardModel, new()
        {
            SpawnEntity<T>();
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

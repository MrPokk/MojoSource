using Game.Engine_Component.CMS;

namespace Game.CMS_Content.Cards
{
    public sealed class BaseCardController : CMSManager
    {
        private HandCards HandCards => GameData<Main>.Boot.HandCards;
        public void GiveCardInHand<T>() where T : BaseCardModel, new()
        {
            if (HandCards.GetCountCard() < HandCards.MaxCard)
                SpawnEntity<T>();
        }
        
        public void GiveCardInHand(BaseCardModel cardModel)
        {
            if (HandCards.GetCountCard() < HandCards.MaxCard)
                SpawnEntity(cardModel);
        }

        public override void DestroyEntity(in ModelView ID)
        {
            HandCards.Remove(ID.GetComponent<BaseCardView>());
            base.DestroyEntity(in ID);
        }
        
        protected override void SpawnEntity<T>()
        {
            Create<T>(out var id);

            var entityCard = GetEntityByID<BaseCardModel>(id);

            entityCard.GetComponent<ViewComponent>(out var viewComponent);
            HandCards.Add(viewComponent.ViewModel as BaseCardView);
        }
        
        private void SpawnEntity(BaseCardModel cardModel)
        {
            Create(cardModel,out var id);

            var entityCard = GetEntityByID<BaseCardModel>(id);

            entityCard.GetComponent<ViewComponent>(out var viewComponent);
            HandCards.Add(viewComponent.ViewModel as BaseCardView);
        }
    }
}

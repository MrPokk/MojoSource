using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using System.Linq;

namespace Game.CMS_Content.Cards.Type
{
    public class MirrorCard : BaseCardModel
    {
        public MirrorCard()
        {
            Components.View.LoadView<BaseCardView>(PathResources.CARD_BASE);
            Components.Priority.Init(Priority.High);
            Components.Action.Init(AbilityCard);
        }

        private void AbilityCard(CMSEntity sourceEntity)
        {
            sourceEntity.GetComponent<AttackComponent>(out var attackComponent);
            if (attackComponent == null)
                return;
            
            var damageCard = attackComponent.GetAllAttack().First();
            damageCard.Components.Action.AbilityCard.Invoke(sourceEntity);
        }
    }
}

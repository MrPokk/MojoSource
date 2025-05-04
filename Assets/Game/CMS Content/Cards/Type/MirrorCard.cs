using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using System.Linq;

namespace Game.CMS_Content.Cards.Type
{
    public class MirrorCard : BaseCardModel
    {
        public MirrorCard()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadView<BaseCardView>(PathResources.CARD);

            Components.PriorityCardComponent.Init(Priority.High);
            Components.ActionCardComponent.Init(AbilityCard);
        }

        private void AbilityCard(CMSEntity sourceEntity)
        {
            sourceEntity.GetComponent<AttackComponent>(out var attackComponent);
            if (attackComponent == null)
                return;
            
            var damageCard = attackComponent.GetAllAttack().First();
            damageCard.Components.ActionCardComponent.AbilityCard.Invoke(sourceEntity);
        }
    }
}

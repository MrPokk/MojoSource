using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Component_Cart.Interactions;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class AttackInteraction : BaseInteraction, IEnterInAttack
    {
        public void UpdateAttack()
        {
            var allEntities = CMS.Get<BaseEntityController>().GetEntities().Values;

            foreach (var entity in allEntities)
            {
                entity.GetComponent<AttackComponent>(out var attackComponent);

                attackComponent?.AddCardToAttack<PlayerModel>(entity);
            }

            ActivationCardsInteraction.ActivateCards();
        }
    }
}

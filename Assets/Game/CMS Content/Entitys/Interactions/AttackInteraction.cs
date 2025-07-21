using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Component_Cart.Interactions;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class AttackInteraction : BaseInteraction, IEnterInAttack, IEnterInNextTurn
    {
        public override Priority PriorityInteraction { get => Priority.Medium; }
        public void UpdateTurn()
        {
            UpdateAttack();
        }
        public void UpdateAttack()
        {
            var allEntities = CMS.Get<BaseEntityController>().GetEntities().Values;

            foreach (var entity in allEntities)
            {
                entity.GetComponent<AttackComponent>(out var attackComponent);

                attackComponent?.OnAttack?.Invoke();
            }
            ActivationCardsInteraction.ActivateCards();
        }
    }
}

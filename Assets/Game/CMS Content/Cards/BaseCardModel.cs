using Engin.Utility;
using Game.CMS_Content.Cards.Components;

namespace Game.CMS_Content.Cards
{
    public abstract class BaseCardModel : CMSEntity
    {
        protected readonly BaseCardComponent CardComponentBase;
        protected BaseCardModel()
        {
            Define<ActionComponent>(out var actionCardComponent);
            Define<PriorityCardComponent>(out var priorityCardComponent);
            CardComponentBase = new BaseCardComponent(actionCardComponent, priorityCardComponent);
        }

        protected class BaseCardComponent : IComponent
        {
            public ActionComponent ActionCardComponent;
            public PriorityCardComponent PriorityCardComponent;
            public BaseCardComponent(ActionComponent actionCard, PriorityCardComponent priorityCard)
            {
                ActionCardComponent = actionCard;
                PriorityCardComponent = priorityCard;
            }
        }
    }
}

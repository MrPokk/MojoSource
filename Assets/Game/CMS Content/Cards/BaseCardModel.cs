using Engin.Utility;
using Game.CMS_Content.Cards.Components;

namespace Game.CMS_Content.Cards
{
    public abstract class BaseCardModel : CMSEntity
    {
        public BaseCardComponent Components { get; private set; }
        protected BaseCardModel()
        {
            Define<ActionComponent>(out var actionCardComponent);
            Define<PriorityCardComponent>(out var priorityCardComponent);
            Define<DraggableComponent>(out var draggableComponent);

            draggableComponent.Drag = dragObject => {
                dragObject.transform.position = MouseInteraction.MousePose;

                if (dragObject is BaseCardView baseCardView)
                    GameData<Main>.Boot.HandCards.Remove(baseCardView);
            };

            draggableComponent.Drop = dragObject => {
                if (dragObject is BaseCardView baseCardView)
                    GameData<Main>.Boot.HandCards.Add(baseCardView);
            };

            Components = new BaseCardComponent(actionCardComponent, priorityCardComponent);
        }



        public class BaseCardComponent : IComponent
        {
            public readonly ActionComponent ActionCardComponent;
            public readonly PriorityCardComponent PriorityCardComponent;
            public BaseCardComponent(ActionComponent actionCard, PriorityCardComponent priorityCard)
            {
                ActionCardComponent = actionCard;
                PriorityCardComponent = priorityCard;
            }
        }
    }
}

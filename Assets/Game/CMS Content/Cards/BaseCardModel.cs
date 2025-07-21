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
            Define<ViewComponent>(out var viewComponent);
            Define<DraggableComponent>(out var draggableComponent);
            
            draggableComponent.Drag = Drag;
            draggableComponent.Drop = Drop;

            Components = new BaseCardComponent(actionCardComponent, priorityCardComponent, viewComponent);
        }
        private void Drag(ModelView dragObject)
        {
            dragObject.transform.position = MouseInteraction.MousePose;

            if (dragObject is BaseCardView baseCardView)
                GameData<Main>.Boot.HandCards.Remove(baseCardView);
        }
        private void Drop(ModelView dragObject)
        {
            if (dragObject is BaseCardView baseCardView)
                GameData<Main>.Boot.HandCards.Add(baseCardView);
        }
        
        public class BaseCardComponent : IComponent
        {
            public readonly ActionComponent Action;
            public readonly PriorityCardComponent Priority;
            public readonly ViewComponent View;

            public BaseCardComponent(ActionComponent actionCard, PriorityCardComponent priorityCard, ViewComponent view)
            {
                Action = actionCard;
                Priority = priorityCard;
                View = view;
            }
        }
    }
}

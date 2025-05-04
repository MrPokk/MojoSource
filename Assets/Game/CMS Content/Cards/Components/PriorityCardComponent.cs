using Engin.Utility;

namespace Game.CMS_Content.Cards.Components
{
    public class PriorityCardComponent : IComponent
    {
        public Priority Priority { get; private set; }
        public void Init(Priority typePriority)
        {
            Priority = typePriority;
        }
    }
}

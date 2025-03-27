using Engin.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Card
{
    public abstract class BaseCardModel : CMSEntity
    {
        protected readonly BaseCardComponent CardComponentBase;
      
        protected BaseCardModel()
        {
            Define<ActionCardComponent>(out var actionCardComponent);
            Define<PriorityCardComponent>(out var priorityCardComponent);
            CardComponentBase = new BaseCardComponent(actionCardComponent, priorityCardComponent);
        }

        protected class BaseCardComponent : IComponent
        {
            public ActionCardComponent ActionCardComponent;
            public PriorityCardComponent PriorityCardComponent;
            public BaseCardComponent(ActionCardComponent actionCard, PriorityCardComponent priorityCard)
            {
                ActionCardComponent = actionCard;
                PriorityCardComponent = priorityCard;
            }
        }
    }
}

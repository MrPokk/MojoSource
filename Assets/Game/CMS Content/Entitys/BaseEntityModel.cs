using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using UnityEngine;

namespace Game.CMS_Content.Entitys
{
    public abstract class BaseEntityModel : CMSEntity
    {
        protected readonly BaseEntityComponent Components;
        protected internal GameObject View => Components.View.ViewModel.gameObject;
        protected BaseEntityModel()
        {
            Define<ViewComponent>(out var viewComponent);
            Define<MoveComponent>(out var moveComponent);
            Define<CardInsideComponent>(out var cardComponent);
            Define<RaycastingComponent>(out var raycastCommand);

            Components = new BaseEntityComponent(moveComponent, viewComponent, cardComponent);
        }

        protected class BaseEntityComponent : IComponent
        {
            public readonly MoveComponent Move;
            public readonly ViewComponent View;
            public readonly CardInsideComponent Card;

            public BaseEntityComponent(MoveComponent moveComponent, ViewComponent viewComponent, CardInsideComponent cardComponent)
            {
                View = viewComponent;
                Move = moveComponent;
                Card = cardComponent;
            }
        }
    }
}

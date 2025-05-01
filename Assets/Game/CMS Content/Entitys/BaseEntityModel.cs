using Engin.Utility;
using Game.CMS_Content.Entitys.Components;
using UnityEngine;

namespace Game.CMS_Content.Entitys
{
    public abstract class BaseEntityModel : CMSEntity
    {
        public BaseEntityComponent Components { get; protected set; }
        protected GameObject ViewObject => Components.View.ViewModel.gameObject;
        protected BaseEntityModel()
        {
            Define<ViewComponent>(out var viewComponent);
            Define<HealthComponent>(out var healthComponent);
            Define<MoveComponent>(out var moveComponent);
            Define<CardInsideComponent>(out var cardComponent);

            Components = new BaseEntityComponent(moveComponent, viewComponent, cardComponent, healthComponent);
        }

        public class BaseEntityComponent : IComponent
        {
            public readonly MoveComponent Move;
            public readonly ViewComponent View;
            public readonly CardInsideComponent Card;
            public readonly HealthComponent Health;


            public BaseEntityComponent(
                MoveComponent moveComponent, 
                ViewComponent viewComponent, 
                CardInsideComponent cardComponent,
                HealthComponent healthComponent
                )
            {
                View = viewComponent;
                Health = healthComponent;
                Move = moveComponent;
                Card = cardComponent;
            }
        }
    }
}

using Engin.Utility;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entitys.Components;
using Game.Script.HUD;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entity
{
    public abstract class BaseEntityModel : CMSEntity
    {
        public List<BaseCardView> InsideCard { get; set; } = new List<BaseCardView>();
        protected readonly BaseEntityComponent BaseComponent;
        
        public GameObject View => BaseComponent.ViewComponent.ViewModel.gameObject;
        
        protected BaseEntityModel()
        {
            Define<ViewComponent>(out var viewComponent);
            Define<MoveComponent>(out var moveComponent);
            Define<CardInsideComponent>(out var cardComponent);
            Define<RaycastingComponent>(out var raycastCommand);
            
            BaseComponent = new BaseEntityComponent(moveComponent,viewComponent,cardComponent);
        }
        
        protected class BaseEntityComponent : IComponent
        {
            public MoveComponent MoveComponent;
            public ViewComponent ViewComponent;
            public CardInsideComponent CardComponent;
  
            public BaseEntityComponent(MoveComponent moveComponent, ViewComponent viewComponent, CardInsideComponent cardComponent)
            {
                ViewComponent = viewComponent;
                MoveComponent = moveComponent;
                CardComponent = cardComponent;
            }
        }
    }
}

using Engin.Utility;
using Game.CMS_Content.Cards;
using Game.CMS_Content.Entity.Components;
using Game.Script.GlobalComponent.Interface;
using Game.Script.HUD;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entity
{
    public abstract class BaseEntityModel : CMSEntity , IContainCard
    {
        public List<BaseCardView> InsideCard { get; set; } = new List<BaseCardView>();
        protected readonly BaseEntityComponent BaseComponent;
        
        public GameObject View => BaseComponent.ViewComponent.ViewModel.gameObject;
        
        protected BaseEntityModel()
        {
            Define<ViewComponent>(out var viewComponent);
            Define<MoveComponent>(out var moveComponent);
            
            viewComponent.LoadView<ViewError>(PathResources.ERORR_VIEW);
            
            BaseComponent = new BaseEntityComponent(moveComponent,viewComponent);
        }
        
        protected class BaseEntityComponent : IComponent
        {
            public MoveComponent MoveComponent;
            public ViewComponent ViewComponent;
  
            public BaseEntityComponent(MoveComponent moveComponent, ViewComponent viewComponent)
            {
                ViewComponent = viewComponent;
                MoveComponent = moveComponent;
            }
        }
    }
}

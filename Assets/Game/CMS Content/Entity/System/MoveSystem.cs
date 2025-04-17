using Game.CMS_Content.Entity.Components;
using Game.CMS_Content.Entity.Type;
using Game.Script.Component_Turn;
using System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.CMS_Content.Entity.System
{
    public class MoveSystem : BaseInteraction
    {
        private void MoveAll()
        {
            
            foreach (var EntityModel in CMS.Get<BaseEntityController>().LoadedEntity)
            {
                EntityModel.Value.Get<MoveComponent>(out var moveComponent);
                
               // moveComponent.MoveMethod?.Invoke(MouseController.MousePose);
            }
        }
    }
}

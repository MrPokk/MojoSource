using Engin.Utility;
using System;
using UnityEngine;

namespace Game.CMS_Content
{

    public class DraggableComponent : RaycastingComponent
    {
        public void Drag(ModelView dragObject)
        {
            dragObject.transform.position = MouseInteraction.MousePose;
        }
    }
}

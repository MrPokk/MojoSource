using Game.CMS_Content;
using System;
using UnityEngine;

namespace Game.Script.Global_Interactions
{
    public class DraggableInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {

        private (ModelView, DraggableComponent) _dragObject = (null, null);

        public void Start()
        {
            RaycastInteraction.ReactionRaycast += ReactionOnRaycast;
        }

        public void Stop()
        {
            RaycastInteraction.ReactionRaycast += ReactionOnRaycast;
        }

        private void ReactionOnRaycast(ModelView objectRaycast)
        {
            objectRaycast.GetModel().GetComponent<DraggableComponent>(out var draggableComponent);
            if (draggableComponent == null)
                return;

            _dragObject.Item1 = objectRaycast;
            _dragObject.Item2 = draggableComponent;

            MouseInteraction.OnClickPressing += UpdateDrag;
            MouseInteraction.OnClickUp += StopDrag;
        }
        private void UpdateDrag(Vector2 mousePosition)
        {
            _dragObject.Item2.Drag(_dragObject.Item1);
        }

        private void StopDrag(Vector2 mousePosition)
        {
            MouseInteraction.OnClickPressing -= UpdateDrag;
        }

    }
}

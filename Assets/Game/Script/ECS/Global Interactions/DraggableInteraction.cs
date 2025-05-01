using Game.CMS_Content;
using Game.Script.ECS.Global_Interactions;
using System;
using UnityEngine;

namespace Game.Script.Global_Interactions
{
    public class DraggableInteraction : BaseInteraction, IEnterInStart, IExitInGame
    {
        public static event Action<ModelView> DropObject;
        public static event Action<ModelView> DraggingObject;

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
            if (!_dragObject.Item1 || _dragObject.Item2 == null)
                return;

            _dragObject.Item2.Drag?.Invoke(_dragObject.Item1);
            DraggingObject?.Invoke(_dragObject.Item1);
        }

        private void StopDrag(Vector2 mousePosition)
        {
            MouseInteraction.OnClickPressing -= UpdateDrag;
            
            if (!_dragObject.Item1 || _dragObject.Item2 == null)
                return;
            
            _dragObject.Item2.Drop?.Invoke(_dragObject.Item1);
            DropObject?.Invoke(_dragObject.Item1);

            _dragObject = (null, null);
        }

    }
}

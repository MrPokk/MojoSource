using Game.Script.GlobalComponent.Interface;
using System;
using UnityEngine;

namespace Game.Script.GlobalComponent
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DraggableObject : ModelView, IRaycasting
    {
        private DraggableObject _dragObject;

        public static event Action<DraggableObject> ReactionOnDrop;
        public static event Action<DraggableObject> ReactionOnDrag;

        private bool _isDrag = false;

        private void OnEnable()
        {
            MouseController.OnClickDown += FirstClick;
            MouseController.OnClickPressing += Drag;
            MouseController.OnClickUp += Drop;
        }
        private void OnDestroy()
        {
            MouseController.OnClickDown -= FirstClick;
            MouseController.OnClickPressing -= Drag;
            MouseController.OnClickUp -= Drop;
        }
        protected virtual void Drop(Vector3 mousePosition)
        {
            if (!_dragObject || !_isDrag)
                return;

            ReactionOnDrop?.Invoke(_dragObject);

            _isDrag = false;
            _dragObject = null;
        }
        protected virtual void Drag(Vector3 mousePosition)
        {
            if (!_dragObject)
                return;

            _isDrag = true;

            ReactionOnDrag?.Invoke(_dragObject);

            _dragObject.transform.position = mousePosition;
        }

        private void FirstClick(Vector3 mousePosition)
        {
            var mRaycastObject = IRaycasting.TryGetRaycastObject<DraggableObject>(mousePosition, out var draggable);
            if (mRaycastObject)
                _dragObject = draggable;
        }

    }
}

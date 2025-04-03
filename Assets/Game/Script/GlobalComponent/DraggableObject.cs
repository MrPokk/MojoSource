using System;
using UnityEngine;

namespace Game.Script.GlobalComponent
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DraggableObject<T> : ModelView<T>
    {
        private DraggableObject<T> _dragObject;

        public static event Action<DraggableObject<T>> ReactionOnDrop;
        public static event Action<DraggableObject<T>> ReactionOnDrag;

        private bool _isDrag = false;

        private void OnEnable()
        {
            MouseController.OnClickDown += GetRaycastObject;
            MouseController.OnClickPressing += Drag;
            MouseController.OnClickUp += Drop;
        }
        private void OnDestroy()
        {
            MouseController.OnClickDown -= GetRaycastObject;
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

        private void GetRaycastObject(Vector3 mousePosition)
        {

            var hitObject = Physics2D.Raycast(mousePosition, Vector2.zero).collider;

            if (hitObject)
                _dragObject = hitObject.gameObject.GetComponent<DraggableObject<T>>();
        }
    }
}

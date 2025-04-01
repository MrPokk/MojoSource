using System;
using UnityEngine;

namespace Game.Script.GlobalComponent
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DraggableObject<T> : ModelView<T>
    {
        private DraggableObject<T> _dragObject;

        public static Action<DraggableObject<T>> ReactionOnDrop;
        public static Action<DraggableObject<T>> ReactionOnDrag;

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
            if (!_isDrag)
                return;
            
            ReactionOnDrop?.Invoke(_dragObject);
            
            _isDrag = false;
            _dragObject = null;
        }
        protected virtual void Drag(Vector3 mousePosition)
        {
            _isDrag = true;
            if (!_dragObject || !_isDrag)
                return;
            
            ReactionOnDrag?.Invoke(_dragObject);
                
            _dragObject.transform.position = mousePosition;
        }

        private void GetRaycastObject(Vector3 mousePosition)
        {
            if (_isDrag)
                return;
            
            var hitObject = Physics2D.Raycast(mousePosition, Vector2.zero).collider.GetComponent<DraggableObject<T>>();
            if (hitObject)
                _dragObject = hitObject;
        }
    }
}

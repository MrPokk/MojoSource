using Engin.Utility;
using System;
using UnityEngine;

namespace Game.CMS_Content
{

    public class DraggableComponent : RaycastingComponent
    {
        private Action<ModelView> _drag;
        private Action<ModelView> _drop;

        public Action<ModelView> Drag {
            get {
                return _drag;
            }
            set {
                if (_drag != null)
                    throw new Exception("DraggableComponent._drag is set");

                _drag = value;
            }
        }
        public Action<ModelView> Drop {
            get {
                return _drop;
            }
            set {
                if (_drop != null)
                    throw new Exception("DraggableComponent._drag is set");

                _drop = value;
            }
        }
    }
}

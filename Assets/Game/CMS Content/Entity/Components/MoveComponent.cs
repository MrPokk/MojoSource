using Engin.Utility;
using Game.CMS_Content.Cards.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.CMS_Content.Entity.Components
{
    public class MoveComponent : IComponent
    {
        public bool IsWalking = false;
        public GameObject ModelViewFromMove;
        private Action<Vector2> _move;
        public Action<Vector2> MoveMethod {
            get {
                return _move;
            }
            set {
                if (_move != null)
                    throw new Exception("Move Entity is set");
                
                _move = value;
            }
        }
    }
}

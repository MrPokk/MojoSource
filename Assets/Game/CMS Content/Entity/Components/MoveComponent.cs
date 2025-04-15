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
        public Action<Vector2>  MoveMethod {
            get {
                return _move;
            }
            set {
                if (_move != null)
                    throw new Exception("Move Entity is set");

                _move = value;
            }
        }


        private bool _isWalking;
        private Action<Vector2> _move;

        public IEnumerator MakeByStep(List<Vector2Int> path, GameObject modelViewFromMove)
        {
            
            if (!_isWalking && path != null)
            {
                _isWalking = true;
                foreach (var Element in path)
                {
                    modelViewFromMove.transform.position = GameData<Main>.Boot.GridController.Grid.ConvertingPosition(Element) + modelViewFromMove.transform.localScale / 2;
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
                yield break;
            
            _isWalking = false;
        }
    }

    public class PositionComponent : IComponent
    {
        public Vector2Int? GridPosition;
        public Vector3 WorldPosition;
    }
}

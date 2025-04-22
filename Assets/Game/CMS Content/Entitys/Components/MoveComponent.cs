using Engin.Utility;
using Game.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class MoveComponent : IComponent
    {
        private bool _isWalking;
        private static int? _maxCountTurn;
        private int _countTurn;
        private Action<Vector2Int> _move;
        
        public MoveComponent()
        {
            CountTurnUpdate();
        }
        public int MaxCountTurn {
            get {
                return _maxCountTurn ?? 0;
            }
            set {
                _maxCountTurn ??= value > 0 ? value : 0;
            }
        }
        public Action<Vector2Int> MoveMethod {
            get {
                return _move;
            }
            set {
                if (_move != null)
                    throw new Exception("Move Entity is set");
                _move = value;
            }
        }
        
        public void CountTurnUpdate()
        {
            _countTurn = MaxCountTurn;
        }
        public IEnumerator MakeByStep(List<Vector2Int> path, GameObject modelViewFromMove)
        {
            if (!_isWalking && path != null && _countTurn > 0)
            {
                _countTurn -= 1;
                _isWalking = true;
                foreach (var Element in path)
                {
                    modelViewFromMove.transform.position = GameData<Main>.Boot.GridController.Grid.ConvertingPosition(Element) + modelViewFromMove.transform.localScale / 2;
                    yield return new WaitForSeconds(AnimationSetting.Instance.SpeedMove);
                }
            }
            else
                yield break;

            _isWalking = false;
        }

    }
}

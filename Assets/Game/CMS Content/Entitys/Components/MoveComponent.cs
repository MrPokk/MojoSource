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
        public int MaxCountTurn { get; private set; }
        public int CountTurn { get; private set; }
        public Action<Vector2Int> MoveMethod { get; private set; }

        public void Init(int maxCountTurn, Action<Vector2Int> moveMethod)
        {
            if (maxCountTurn > 0)
                MaxCountTurn = maxCountTurn;
            else
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");
            MoveMethod = moveMethod;
            CountTurnUpdate();
        }
        public void CountTurnUpdate()
        {
            CountTurn = MaxCountTurn;
        }
        public IEnumerator MakeByStep(List<Vector2Int> path, GameObject modelViewFromMove)
        {
            if (!_isWalking && path != null && CountTurn > 0)
            {
                CountTurn -= 1;
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

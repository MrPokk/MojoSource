using Engin.Utility;
using Game.Script.Component_Grid.Component_Pathfind;
using Game.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class MoveComponent : IComponent
    {
        public bool IsWalking { get; private set; }
        public int MaxCountTurn { get; private set; }
        public int CountTurn { get; private set; }
        public Action<Vector2Int> MoveMethod { get; private set; }

        public void Init(int maxCountTurn, Action<Vector2Int> moveMethod)
        {
            if (maxCountTurn > 0)
                MaxCountTurn = maxCountTurn;
            else
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");
            
            MoveMethod ??= moveMethod;
   
            CountTurnUpdate();
        }
        public void CountTurnUpdate()
        {
            CountTurn = MaxCountTurn;
        }
        public void MakeByStep(List<Vector2Int> path, Vector2Int startPosition, Vector2Int endPosition, GameObject modelViewFromMove)
        {
            GridUtility.SetTypeInGrid(startPosition, GridNode.TypeNode.SimplyNode);

            if (!IsWalking && path != null && CountTurn > 0)
            {
                IsWalking = true;
                GameData<Main>.Coroutine.Run(MoveByStepsWithDelay(path, modelViewFromMove));
            }
            else
                return;

            GridUtility.SetTypeInGrid(endPosition, GridNode.TypeNode.Wall);
        }

        private IEnumerator MoveByStepsWithDelay(List<Vector2Int> path, GameObject modelViewFromMove)
        {
            if (path != null && path.Count != 0 && IsWalking && CountTurn > 0)
            {
                --CountTurn;
                foreach (var element in path)
                {
                    modelViewFromMove.transform.position = GridUtility.ConvertingPosition(element) + modelViewFromMove.transform.localScale / 2;
                    yield return new WaitForSeconds(AnimationSetting.Instance.SpeedMove);
                }
            }
            IsWalking = false;
        }
    }
}

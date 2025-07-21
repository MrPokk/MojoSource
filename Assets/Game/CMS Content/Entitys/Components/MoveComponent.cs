using Engin.Utility;
using Game.Script.Component_Grid.Component_Pathfind;
using Game.Script.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class MoveComponent : IComponent
    {
        public bool IsWalking { get; private set; }
        public Vector2Int[] NeighborsOffset { get; private set; }
        
        private int _maxCountStep;
        private int _countStep;
        public Action<Vector2Int> MoveMethod { get; private set; }
       

        public GridController GridController { get; private set; }


        public void Init(int maxCountStep, in Vector2Int[] neighborsAll, Action<Vector2Int> moveMethod, GridController gridController)
        {
            if (maxCountStep > 0)
                _maxCountStep = maxCountStep;
            else
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");

            MoveMethod ??= moveMethod;
            GridController ??= gridController;
            NeighborsOffset = neighborsAll;

            CountTurnUpdate();
        }
        public void CountTurnUpdate()
        {
            _countStep = _maxCountStep;
        }
        public void MakeByStep(List<Vector2Int> path, ModelView modelViewFromMove)
        {
            if (!modelViewFromMove || path == null || path.First() == path.Last())
                return;

            if (IsWalking || _countStep <= 0)
                return;

            IsWalking = true;
            GameData<Main>.Coroutine.Run(MoveByStepsWithDelay(path, modelViewFromMove));
        }

        private IEnumerator MoveByStepsWithDelay(List<Vector2Int> path, ModelView modelViewFromMove)
        {

            GridController.SetTypeInGrid(modelViewFromMove.transform.position, GridNode.TypeNode.SimplyNode);

            if (path.Any() && IsWalking && _countStep > 0)
            {
                foreach (var element in path)
                {
                    if (_countStep < 0)
                        break;

                    modelViewFromMove.transform.position = GridController.ConvertingPosition(element) + modelViewFromMove.transform.localScale / 2;
                    yield return new WaitForSeconds(AnimationSetting.Instance.SpeedMove);

                    _countStep--;
                }
            }
            
            IsWalking = false;

            GridController.SetTypeInGrid(modelViewFromMove.transform.position, GridNode.TypeNode.Wall);
        }
    }
}

using Engin.Utility;
using Game.CMS_Content.Cards.Components;
using Game.CMS_Content.Entity.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entity.Type
{
    public class PlayerModel : BaseEntityModel
    {
        private bool IsWalking {
            get => BaseComponent.MoveComponent.IsWalking;
            set {
                BaseComponent.MoveComponent.IsWalking = value;
            }
        }
        private GameObject ModelView => BaseComponent.MoveComponent.ModelViewFromMove;

        public PlayerModel()
        {
            BaseComponent.ViewComponent.LoadView<PlayerView>(PathResources.ENTITY);
            BaseComponent.MoveComponent.MoveMethod = Move;
            BaseComponent.MoveComponent.ModelViewFromMove = BaseComponent.ViewComponent.ViewModel.gameObject ;
            BaseComponent.MoveComponent.IsWalking = false;
        }

        private void Move(Vector2 mousePosition)
        {
            
            var MousePoseInGrid = GridUtility.TryGetPositionInGrid(mousePosition, GameData<Main>.Boot.GridController.Grid);
            var PlayerPoseInGrid = GridUtility.TryGetPositionInGrid(ModelView.transform.position, GameData<Main>.Boot.GridController.Grid);

            if (MousePoseInGrid == null || PlayerPoseInGrid == null)
                return;

            var Path = AStar.TryGetPathFind((Vector2Int)PlayerPoseInGrid, (Vector2Int)MousePoseInGrid, GameData<Main>.Boot.GridController.Grid.Array);

            if (Path != null && IsWalking == false)
                Main._coroutine.StartCoroutine(MakeByStep(Path));
        }

        private IEnumerator MakeByStep(List<Vector2Int> path)
        {
            IsWalking = true;
            foreach (var Element in path)
            {
                ModelView.transform.position = GameData<Main>.Boot.GridController.Grid.ConvertingPosition(Element) + ModelView.transform.localScale / 2;
                yield return new WaitForSeconds(0.5f);
            }
            IsWalking = false;
        }
    }
}

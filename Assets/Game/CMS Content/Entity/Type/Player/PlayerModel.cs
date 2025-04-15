using System;
using UnityEngine;

namespace Game.CMS_Content.Entity.Type
{
    public class PlayerModel : BaseEntityModel
    {
        
        
        public PlayerModel()
        {
            BaseComponent.ViewComponent.LoadView<PlayerView>(PathResources.ENTITY);

            BaseComponent.MoveComponent.MoveMethod = MoveToMouse;
        }
        private void MoveToMouse(Vector2 mousePosition)
        {
            var MousePoseInGrid = GridUtility.TryGetPositionInGrid(MouseController.MousePose, GameData<Main>.Boot.GridController.Grid);
            var GridPosition = GridUtility.TryGetPositionInGrid(View.transform.position, GameData<Main>.Boot.GridController.Grid);

            if (MousePoseInGrid == null || GridPosition == null)
                return;

            var Path = AStar.TryGetPathFind((Vector2Int)GridPosition, (Vector2Int)MousePoseInGrid, GameData<Main>.Boot.GridController.Grid.Array);

            GameData<Main>.Corotine.StartCoroutine(BaseComponent.MoveComponent.MakeByStep(Path, View));
        }
    }
}

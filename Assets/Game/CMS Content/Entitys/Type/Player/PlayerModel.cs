using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.CMS_Content.Entity.Type
{
    public class PlayerModel : BaseEntityModel
    {
        public PlayerModel()
        {
            
            BaseComponent.ViewComponent.LoadView<PlayerView>(PathResources.ENTITY);
            BaseComponent.MoveComponent.MoveMethod = MoveTo;
        }
     
        
        private void MoveTo(Vector2Int positionTo)
        {
            var IsStartPosition = GridUtility.TryGetPositionInGrid(View.transform.position, out var StartPosition);
            var EndPosition = GridUtility.IsWithinGrid(positionTo);

            if (!IsStartPosition || !EndPosition)
                return;

            var Path = AStar.TryGetPathFind(StartPosition, positionTo, GameData<Main>.Boot.GridController.Grid.Array);

            GameData<Main>.Corotine.StartCoroutine(BaseComponent.MoveComponent.MakeByStep(Path, View));
        }
    }
}

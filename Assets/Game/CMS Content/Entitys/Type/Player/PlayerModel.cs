using Game.CMS_Content.Entity.Type;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Player
{
    public class PlayerModel : BaseEntityModel
    {
        public PlayerModel()
        {
            Components.View.LoadView<PlayerView>(PathResources.ENTITY);
            Components.Move.Init(2,MoveTo);
        }
     
        
        private void MoveTo(Vector2Int positionTo)
        {
            var IsStartPosition = GridUtility.TryGetPositionInGrid(View.transform.position, out var StartPosition);
            var EndPosition = GridUtility.IsWithinGrid(positionTo);

            if (!IsStartPosition || !EndPosition)
                return;

            var Path = AStar.TryGetPathFind(StartPosition, positionTo, GameData<Main>.Boot.GridController.Grid.Array);

            GameData<Main>.Corotine.StartCoroutine(Components.Move.MakeByStep(Path, View));
        }
    }
}

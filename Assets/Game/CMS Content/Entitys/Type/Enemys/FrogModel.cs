using Game.CMS_Content.Entity.Type;
using Game.CMS_Content.Entity.Type.Enemys;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.CMS_Content.Entitys.Type.Player;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Enemys
{
    public class FrogModel : BaseEntityModel, IEnemy
    {
        public FrogModel()
        {
            Define<DraggableComponent>(out DraggableComponent draggableComponent);
            draggableComponent.Drag = dragObject => dragObject.transform.position = MouseInteraction.MousePose;
            Components.View.LoadView<FrogView>(PathResources.ENTITY);
            Components.Move.Init(2, MoveTo);
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

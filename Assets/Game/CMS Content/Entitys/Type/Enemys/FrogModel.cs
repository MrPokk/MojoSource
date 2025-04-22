using Game.CMS_Content.Entitys;
using Game.CMS_Content.Entitys.Components;
using UnityEngine;

namespace Game.CMS_Content.Entity.Type.Enemys
{
    public class FrogModel : BaseEntityModel
    {
        public FrogModel()
        {
            BaseComponent.ViewComponent.LoadView<FrogView>(PathResources.ENTITY);
            BaseComponent.MoveComponent.MoveMethod = MoveTo;
            BaseComponent.MoveComponent.MaxCountTurn = 2;
        }
        
        private void MoveTo(Vector2Int positionTo)
        {
            var Entitys = CMS.Get<BaseEntityController>().GetEntities();

            foreach (var AllEntity in Entitys.Values)
            {
                if (AllEntity is not PlayerModel Player)
                    continue;

                var IsPlayerPoseInGrid = GridUtility.TryGetPositionInGrid(Player.View.transform.position, out var player);
                var IsEnemyPoseInGrid = GridUtility.TryGetPositionInGrid(View.transform.position, out var enemy);

                if (!IsPlayerPoseInGrid || !IsEnemyPoseInGrid)
                    return;

                var PathToPlayer = AStar.TryGetPathFind(player, enemy);

                GameData<Main>.Corotine.StartCoroutine(BaseComponent.MoveComponent.MakeByStep(PathToPlayer, View));
            }
        }
    }
}

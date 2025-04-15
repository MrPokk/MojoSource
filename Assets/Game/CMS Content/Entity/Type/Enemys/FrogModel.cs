using Engin.Utility;
using Game.CMS_Content.Entity.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CMS_Content.Entity.Type.Enemys
{
    public class FrogModel : BaseEntityModel
    {
        public FrogModel()
        {
            // BaseComponent.ViewComponent.LoadView<PlayerView>(PathResources.ENTITY);
            
            BaseComponent.MoveComponent.MoveMethod = Move;
        }
      


        private void Move(Vector2 mousePosition)
        {
            var Entitys = CMS.Get<BaseEntityController>().LoadedEntity;

            foreach (var AllEntity in Entitys.Values)
            {
                if (AllEntity is not PlayerModel Player)
                    continue;
                
                var PlayerPoseInGrid =  GridUtility.TryGetPositionInGrid(Player.View.transform.position, GameData<Main>.Boot.GridController.Grid);;
                var EnemyPoseInGrid =  GridUtility.TryGetPositionInGrid(View.transform.position, GameData<Main>.Boot.GridController.Grid);;
                    
                if (PlayerPoseInGrid == null || EnemyPoseInGrid == null)
                    return;
                
                if(PlayerPoseInGrid == EnemyPoseInGrid)
                    return;
                   
                var PathToPlayer = AStar.TryGetPathFind((Vector2Int)EnemyPoseInGrid, (Vector2Int)PlayerPoseInGrid, GameData<Main>.Boot.GridController.Grid.Array);

                GameData<Main>.Corotine.StartCoroutine(BaseComponent.MoveComponent.MakeByStep(PathToPlayer, View));
            }
        }
    }
}

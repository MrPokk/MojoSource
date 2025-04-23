using Game.CMS_Content.Entitys.Components;
using Game.CMS_Content.Entitys.Type.Interfaces;
using Game.CMS_Content.Entitys.Type.Player;
using Game.Script.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Interactions
{
    public class MoveInteraction : BaseInteraction, IEnterInNextTurn
    {
        public void UpdateTurn()
        {
            IEnumerable<CMSEntity> AllEntities = CMS.Get<BaseEntityController>().GetEntities().Values;
            var PlayerEntityModel = AllEntities.Where(x => x is PlayerModel);

            foreach (var Entity in AllEntities)
            {
                Entity.GetComponent<MoveComponent>(out var moveComponent);
                moveComponent.CountTurnUpdate();

                if (Entity is IEnemy)
                {
                    var NearestPlayer = TransformUtility.FindToNearest<PlayerModel>(Entity.GetView());
                    var PositionPlayer = NearestPlayer.GetViewPosition2D();
                    GridUtility.TryGetPositionInGrid(PositionPlayer, out var positionInGrid);
                    moveComponent.MoveMethod.Invoke(positionInGrid);
                }
            }
        }
    }
}

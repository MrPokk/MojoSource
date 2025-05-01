using Game.CMS_Content.Entity.Type;
using Game.CMS_Content.Entity.Type.Enemys;
using Game.CMS_Content.Entitys.Type.Enemys;
using Game.CMS_Content.Entitys.Type.Player;
using UnityEngine;

namespace Game.CMS_Content.Entitys
{
    public class SystemInitEntity : SystemInit
    {
        protected override void Init()
        {
            var controller = CMS.Get<BaseEntityController>();

            controller.SpawnEntityInGrid<PlayerModel>(new Vector2Int(0, 0));
            controller.SpawnEntityInGrid<PlayerModel>(new Vector2Int(1, 1));
            controller.SpawnEntityInGrid<FrogModel>(new Vector2Int(3, 3));
            controller.SpawnEntityInGrid<FrogModel>(new Vector2Int(5, 1));
        }
    }
}

using Game.CMS_Content.Entity.Type;
using Game.CMS_Content.Entity.Type.Enemys;
using Game.CMS_Content.Entitys;
using UnityEngine;

namespace Game.CMS_Content.Entity
{
    public class SystemInitEntity : SystemInit
    {
        protected override void Init()
        {
            var Controller = CMS.Get<BaseEntityController>();

            Controller.SpawnEntityInGrid<PlayerModel>(new Vector2Int(0, 0));
            Controller.SpawnEntityInGrid<FrogModel>(new Vector2Int(5, 5));

        }
    }
}

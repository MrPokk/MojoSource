using Game.CMS_Content.Entity.Type;
using UnityEngine;

namespace Game.CMS_Content.Entity
{
    public class SystemInitEntity : SystemInit
    {
        protected override void Init()
        {
            var Controller = CMS.Get<BaseEntityController>();
            
            Controller.SpawnEntityInGrid<PlayerModel>(new Vector2Int(0, 0));
        }
    }
}

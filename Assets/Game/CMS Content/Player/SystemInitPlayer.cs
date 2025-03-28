using Game.CMS_Content;
using Game.CMS_Content.Card;
using UnityEngine;



namespace Game.CMS_Content.Player
{
    public class SystemInitPlayer : SystemInit
    {
        protected override void Init()
        {
            CMS.TryGetComponent<EntityPlayer, ViewComponent>(out var viewComponent);

            Vector3 PositionInGrid = GridUtility.TryGetPositionInGrid(new Vector2Int(0, 0), GameData<Main>.Boot.GridController.Grid);
            PositionInGrid += (viewComponent.Prefab.transform.localScale / 2);

            GameData<Main>.Boot.InstantiateCMSEntity(in viewComponent, PositionInGrid);
        }
    }
}

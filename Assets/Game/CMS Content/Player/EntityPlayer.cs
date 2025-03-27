using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Card;
using UnityEngine;


namespace Game.CMS_Content.Player
{
    public class EntityPlayer : CMSEntity
    {
        public EntityPlayer()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadModel<EntityView>(PathResources.PREFABS);

        }
    }
}

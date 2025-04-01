using Engin.Utility;
using Game.CMS_Content;
using Game.CMS_Content.Card;
using Game.Script.GlobalComponent.Interface;
using System.Collections.Generic;
using UnityEngine;


namespace Game.CMS_Content.Player
{
    public class EntityPlayer : CMSEntity , IContainCard
    {
        public List<BaseCardView> InsideCard { get; set; }
        public EntityPlayer()
        {
            Define(out ViewComponent viewComponent);
            viewComponent.LoadModel<EntityView>(PathResources.PREFABS);

        }
        
    }
}

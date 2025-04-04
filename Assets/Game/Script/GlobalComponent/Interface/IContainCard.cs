using Game.CMS_Content.Cards;
using System.Collections.Generic;

namespace Game.Script.GlobalComponent.Interface
{
    public interface IContainCard 
    {
        public List<BaseCardView> InsideCard { get; set; }
    }
}

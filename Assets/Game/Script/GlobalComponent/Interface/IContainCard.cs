using Game.CMS_Content.Card;
using System.Collections.Generic;

namespace Game.Script.GlobalComponent.Interface
{
    public interface IContainCard
    {
        public List<BaseCardView> InsideCard { get; set; }
    }
}

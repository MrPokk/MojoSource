using Engin.Utility;
using System;

namespace Game.CMS_Content.Cards.Components
{
    public class ActionComponent : IComponent
    {
        public Action<CMSEntity> AbilityCard { get; private set; }
        public void Init( Action<CMSEntity> abilityCard)
        {
            AbilityCard ??= abilityCard;
        }
    }
}

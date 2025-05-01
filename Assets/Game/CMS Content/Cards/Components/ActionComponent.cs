using Engin.Utility;
using System;

namespace Game.CMS_Content.Cards.Components
{
    public class ActionComponent : IComponent
    {
        private Action<CMSEntity> _abilityCard;
        public Action<CMSEntity> AbilityCard {
            get {
                return _abilityCard;
            }
            set {
                if (_abilityCard != null)
                    throw new Exception("ActionCardComponent.AbilityCard is set");
                
                _abilityCard = value;
            }
        }
    }
}

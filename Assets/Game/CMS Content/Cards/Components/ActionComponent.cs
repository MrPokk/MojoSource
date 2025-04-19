using Engin.Utility;
using System;

namespace Game.CMS_Content.Cards.Components
{
    public class ActionComponent : IComponent
    {
        private Action _abilityCard;
        public Action AbilityCard {
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

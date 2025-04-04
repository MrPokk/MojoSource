using Engin.Utility;
using System;
using UnityEngine;

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

    public class PriorityCardComponent : IComponent
    {
        public Priority Priority;
    }

    public class SpriteCardComponent : IComponent
    {
        public Sprite Front;
        public Sprite Back;
    }
}

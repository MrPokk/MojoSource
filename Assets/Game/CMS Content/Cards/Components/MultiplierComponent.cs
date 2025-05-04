using Engin.Utility;
using System;

namespace Game.CMS_Content.Cards.Components
{
    public class MultiplierComponent : IComponent
    {
        public int MultiplierMax { get; private set; }
        public int MultiplierCurrent { get; private set; }
        public void Init(int multiplierFactorMax)
        {
            if (multiplierFactorMax > 0)
                MultiplierMax = multiplierFactorMax;
            else
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");

            SetMultiplier();
        }

        public int UsingMultiplier()
        {
            var factor = MultiplierCurrent;
            SetMultiplier();
            return factor;
        }

        public void SetMultiplier(int factor = 1)
        {
            if (factor <= 0)
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");
            
            if (factor > MultiplierMax)
                MultiplierCurrent = MultiplierMax;

            MultiplierCurrent = factor;
        }
    }
}

using Engin.Utility;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class HealthComponent : IComponent
    {
        public int MaxHealth { get; private set; }
        public int HealthCurrent { get; private set; }
        public void Init(int maxHealth)
        {
            MaxHealth = maxHealth;
            HealthCurrent = MaxHealth;
        }

        public void Decrease(uint countHealth)
        {
            var deltaHealth = (HealthCurrent - countHealth);
            if (deltaHealth < 0)
                HealthCurrent = 0;
            else
                HealthCurrent = (int)deltaHealth;
        }

        public void Increase(uint countHealth)
        {
            var deltaHealth = (HealthCurrent + countHealth);
            if (deltaHealth > MaxHealth)
                HealthCurrent = MaxHealth;
            else
                HealthCurrent = (int)deltaHealth;
        }

    }
}

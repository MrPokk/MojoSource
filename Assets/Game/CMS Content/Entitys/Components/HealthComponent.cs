using Engin.Utility;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Components
{
    public class HealthComponent : IComponent
    {
        public event Action<int> OnChange;
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

            HealthCurrent = (int)deltaHealth;
            OnChange?.Invoke(HealthCurrent);
        }

        public void Increase(uint countHealth)
        {
            var deltaHealth = (HealthCurrent + countHealth);
            if (deltaHealth > MaxHealth)
                HealthCurrent = MaxHealth;
            
            HealthCurrent = (int)deltaHealth;
            OnChange?.Invoke(HealthCurrent);
        }

    }
}

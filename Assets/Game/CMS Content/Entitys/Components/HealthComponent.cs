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
            if (maxHealth > 0)
                MaxHealth = maxHealth;
            else
                throw new ArgumentException("ERROR: THE VALUES MUST BE GREATER THAN 0");
        
            HealthCurrent = MaxHealth;
        }
        
        public void Decrease(int countHealth)
        {
            var deltaHealth = (HealthCurrent - countHealth);
            HealthCurrent = deltaHealth <= 0 ? 0 : deltaHealth;
            
            OnChange?.Invoke(HealthCurrent);
        }

        public void Increase(int countHealth)
        {
            var deltaHealth = (HealthCurrent + countHealth);
            HealthCurrent = deltaHealth > MaxHealth ? MaxHealth : deltaHealth;

            OnChange?.Invoke(HealthCurrent);
        }
        
    }
}

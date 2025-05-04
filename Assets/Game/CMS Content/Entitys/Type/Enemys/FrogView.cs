using Game.CMS_Content.Entitys.Components;
using TMPro;
using UnityEngine;

namespace Game.CMS_Content.Entitys.Type.Enemys
{
    public class FrogView : ModelView
    {
        [field: SerializeField]
        public TMP_Text Health { get; private set; }
        private HealthComponent _healthComponent;

        private void Start()
        {
            GetModel().GetComponent<HealthComponent>(out var healthComponent);
            _healthComponent = healthComponent;
            _healthComponent.OnChange += HealthComponentChange;

            Health.text = _healthComponent.HealthCurrent.ToString();
        }
        private void OnDestroy()
        {
            _healthComponent.OnChange -= HealthComponentChange;
        }
        private void HealthComponentChange(int value)
        {
            Health.text = value.ToString();
        }
    }
}

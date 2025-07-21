using System;
using TMPro;
using UnityEngine;

namespace Game.CMS_Content.Cards
{
    public class BaseCardView : ModelView
    {
        [SerializeField]
        private TMP_Text _name;

        private void Start()
        {
            _name.text = GetModel().ID.Name;
        }
    }
}

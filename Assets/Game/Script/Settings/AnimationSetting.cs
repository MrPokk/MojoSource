using Game.Script.Utility.FromEditor;
using System;
using UnityEngine;

namespace Game.Script.Settings
{

    [CreateAssetMenu(fileName = "AnimationSetting", menuName = "PROJECT_SETTING/AnimationSetting", order = 1)]
    public class AnimationSetting : ScriptableGlobalSetting<AnimationSetting>
    {
        [field: SerializeField]
        public float SpeedMove { get; private set; }

        private void OnValidate()
        {
            if (SpeedMove < 0)
                SpeedMove = 0;
        }
    }
}

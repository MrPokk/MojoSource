using Game.Script.Utility.FromEditor;
using UnityEngine;

namespace Game.Script.Settings
{
    [CreateAssetMenu(fileName = "TurnSetting", menuName = "PROJECT_SETTING/TurnSetting", order = 1)]
    public class TurnSetting : ScriptableGlobalSetting<TurnSetting>
    {

        [SerializeField] private int _countTurn;

    }
}

using Game.Script.ECS.Global_Interactions;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public void NextTurn()
    {
        GameData<Main>.Turn.NextTurn();
    }

}

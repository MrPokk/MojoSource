using Engin.Utility;
using Game.Script.Utility;
using UnityEngine;

public static class GameData<T> where T : IMain
{
    public static T Boot;
    public static CoroutineUtility Corotine;

}

using Engin.Utility;
using UnityEngine;

public static class GameData<T> where T : IMain
{
    public static T Boot;
    public static bool IsStartGame;
}

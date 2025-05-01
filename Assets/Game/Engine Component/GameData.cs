using Engin.Utility;
using Game.Script.ECS.Global_Interactions;
using Game.Script.Utility;
using Game.Script.Utility.FromGame;
using JetBrains.Annotations;

public static class GameData<T> where T : IMain
{
    [NotNull] public static T Boot;
    [NotNull] public static CoroutineUtility.CoroutineRunner Coroutine;
    [NotNull] public static TurnInteraction Turn;
}

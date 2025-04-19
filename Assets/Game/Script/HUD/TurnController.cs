using System;

namespace Game.Script.HUD
{
    public class TurnController
    {
        private int _count;
        private object _senderTurn;

        public static event Action OnEndTurn;

        public void EndTurn()
        {
            OnEndTurn?.Invoke();

        }
    }
}

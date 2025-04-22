using System;

namespace Game.Script.HUD
{
    public class TurnController : BaseInteraction, IEnterInNextTurn
    {
        private int _count;
        private object _senderTurn;
        public static event Action UpdateInTurn;
        
        public void UpdateTurn()
        {
            UpdateInTurn?.Invoke();
        }
    }
}

using Game.CMS_Content.Entity.Type;
using System;

namespace Game.Script.Component_Turn
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

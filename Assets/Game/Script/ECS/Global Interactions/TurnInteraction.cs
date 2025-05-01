using Game.CMS_Content.Entitys.Interactions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Script.ECS.Global_Interactions
{
    public sealed class TurnInteraction : BaseInteraction
    {
        public enum TurnState
        {
            Inactive,
            Processing,
            Finished
        }

        public TurnState CurrentState { get; private set; } = TurnState.Inactive;

        public static event Action OnTurnStarted;
        public static event Action OnTurnEnded;

        public void NextTurn()
        {
            switch (CurrentState)
            {
                case TurnState.Inactive:
                    StartTurn();
                    break;
                case TurnState.Processing:
                    break;
                case TurnState.Finished:
                    StartTurn();
                    break;
            }
        }

        private void StartTurn()
        {
            CurrentState = TurnState.Processing;
            OnTurnStarted?.Invoke();

            foreach (var nextTurnElement in InteractionCache<IEnterInNextTurn>.AllInteraction)
            {
                nextTurnElement.UpdateTurn();
            }
            EndTurn();
        }

        private void EndTurn()
        {
            CurrentState = TurnState.Finished;
            OnTurnEnded?.Invoke();

            GameData<Main>.Coroutine.StopAll(() => CurrentState = TurnState.Inactive);
        }

    }
}

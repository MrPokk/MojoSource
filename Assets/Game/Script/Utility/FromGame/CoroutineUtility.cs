using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Script.Utility.FromGame
{
    public class CoroutineUtility : MonoBehaviour
    {
        private readonly Queue<Coroutine> _activeCoroutines = new Queue<Coroutine>();
        private event Action OnFinishedAll;
        private Coroutine RunCoroutine(IEnumerator coroutine)
        {
            Coroutine coroutineInstance = StartCoroutine(InternalRunCoroutine(coroutine));
            _activeCoroutines.Enqueue(coroutineInstance);

            return coroutineInstance;
        }

        private IEnumerator InternalRunCoroutine(IEnumerator coroutine)
        {
            yield return coroutine;
            _activeCoroutines.Dequeue();
            if (_activeCoroutines.Count <= 0)
                OnFinishedAll?.Invoke();
        }
        public sealed class CoroutineRunner
        {
            private readonly CoroutineUtility _utility;

            public CoroutineRunner(CoroutineUtility utility)
            {
                _utility = utility;
            }

            public void Run(IEnumerator coroutine)
            {
                _utility.RunCoroutine(coroutine);
            }

            public void StopAll(Action callBack)
            {
                _utility.OnFinishedAll += callBack;
            }

        }
    }

}

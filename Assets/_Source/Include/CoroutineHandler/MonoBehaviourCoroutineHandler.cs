using System.Collections;
using UnityEngine;

namespace _Source.Include
{
    public class MonoBehaviourCoroutineHandler : ICoroutineHandler
    {
        private MonoBehaviour _monoBehaviour;

        public MonoBehaviourCoroutineHandler(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        public Coroutine StartCoroutine(IEnumerator routine) => _monoBehaviour.StartCoroutine(routine);

        public void StopCoroutine(Coroutine routine) => _monoBehaviour.StopCoroutine(routine);
    }
}
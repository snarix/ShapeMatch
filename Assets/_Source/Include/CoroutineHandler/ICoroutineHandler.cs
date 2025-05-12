using System.Collections;
using UnityEngine;

namespace _Source.Include
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}
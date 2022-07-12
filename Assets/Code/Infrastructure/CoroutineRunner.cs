using System.Collections;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }

            Destroy(this.gameObject);
        }

        public static Coroutine StartNewRoutine(IEnumerator coroutine)
        {
            if (coroutine == null)
            {
                Debug.Log("EmptyCoroutineTruingToSstart");
            }

            return instance.StartCoroutine(coroutine);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                instance.StopCoroutine(coroutine);
            }
            else
            {
                Debug.Log("EmptyCoroutineTruingToStop");
            }
        }

        public static void StopAllRoutines()
        {
            instance.StopAllCoroutines();
        }
    }
}
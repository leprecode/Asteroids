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

        public static void StartRoutine(IEnumerator coroutine)
        {
            instance.StartCoroutine(coroutine);
        }
    }
}
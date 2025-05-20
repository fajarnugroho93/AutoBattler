using System.Collections;
using UnityEngine;

namespace SpaceKomodo.Utilities
{
    public static class MonoBehaviourHandler
    {
        private static MonoBehaviourProxy _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance == null)
            {
                var gameObject = new GameObject(nameof(MonoBehaviourProxy));
                Object.DontDestroyOnLoad(gameObject);
                _instance = gameObject.AddComponent<MonoBehaviourProxy>();
            }
        }

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            if (_instance == null)
            {
                Initialize();
            }
            
            return _instance.StartCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            if (_instance != null && coroutine != null)
            {
                _instance.StopCoroutine(coroutine);
            }
        }

        public static void StopAllCoroutines()
        {
            if (_instance != null)
            {
                _instance.StopAllCoroutines();
            }
        }
    }
}
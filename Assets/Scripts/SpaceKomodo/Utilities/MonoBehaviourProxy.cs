using UnityEngine;

namespace SpaceKomodo.Utilities
{
    public class MonoBehaviourProxy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
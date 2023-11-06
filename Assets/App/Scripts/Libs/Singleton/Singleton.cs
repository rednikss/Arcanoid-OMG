using UnityEngine;

namespace App.Scripts.Libs.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        public void Init()
        {
            if (Instance != null) Destroy(this);

            Instance = (T) (object) this;
        }
    }
}
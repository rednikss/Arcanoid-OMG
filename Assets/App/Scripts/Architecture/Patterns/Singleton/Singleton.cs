using App.Scripts.Architecture.InitPoint.MonoInitializable;
using UnityEngine;

namespace App.Scripts.Architecture.Patterns.Singleton
{
    public class Singleton<T> : MonoInitializable where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        public override void Init()
        {
            if (Instance != null) Destroy(this);

            Instance = (T) (object) this;
        }
    }
}
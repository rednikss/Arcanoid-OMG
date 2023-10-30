using UnityEngine;

namespace App.Scripts.Architecture.MonoInitializable
{
    public abstract class MonoInitializable : MonoBehaviour, IInitializable
    {
        public abstract void Init();
    }
}
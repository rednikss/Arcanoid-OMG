using UnityEngine;

namespace App.Scripts.Architecture.InitPoint.MonoInitializable
{
    public abstract class MonoInitializable : MonoBehaviour, IInitializable
    {
        public abstract void Init();
    }
}
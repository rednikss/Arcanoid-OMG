using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Libs.EntryPoint.MonoInstaller
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Init(ServiceContainer container);
    }
}
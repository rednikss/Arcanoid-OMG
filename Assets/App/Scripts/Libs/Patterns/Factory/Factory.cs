using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.Factory
{
    public abstract class Factory<TObjectType> : MonoInstaller, IFactory<TObjectType>
    {
        [SerializeField] protected TObjectType prefab;
        
        public abstract TObjectType Create();
    }
}
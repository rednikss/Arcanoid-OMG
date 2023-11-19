using System;
using App.Scripts.Libs.Patterns.ObjectPool;

namespace App.Scripts.Architecture.Scene.LevelObjectPool
{
    public interface ILevelObjectPool<TObjectType> : IObjectPool<TObjectType>
    {
        public Action GetOnReturnAllEvent();
    }
}
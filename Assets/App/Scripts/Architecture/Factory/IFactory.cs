using UnityEngine;

namespace App.Scripts.Architecture.Factory
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
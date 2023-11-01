using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Architecture.Patterns.ServiceLocator
{
    public class ServiceLocator : Singleton.Singleton<ServiceLocator>
    {
        private readonly Dictionary<string, IService> _services = new();
        
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;

            if (_services.ContainsKey(key))
            {
                Debug.LogError("Attempted to add existing service {key}!");
                return;
            }
            
            _services.Add(key, service);
        }
        
        public void Unregister<T>(T service) where T : IService
        {
            string key = typeof(T).Name;

            if (!_services.ContainsKey(key))
            {
                Debug.LogError("Attempted to remove non-existent service {key}!");
                return;
            }
            
            _services.Remove(key);
        }
        
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;

            if (!_services.ContainsKey(key))
            {
                Debug.LogError("Attempted to get non-existent service {key}!");
                throw new InvalidOperationException();
            }
            
            return (T) _services[key];
        }
    }
}
using System.Collections.Generic;

namespace App.Scripts.Libs.Patterns.Service.Container
{
    public class ServiceContainer
    {
        private readonly Dictionary<string, IService> _services = new();
        
        public void SetServiceSelf<TService>(TService service)
        {
            SetService<TService, TService>(service);
        }
        
        public void SetService<TBind, TService>(TService service) where TService : TBind
        {
            var container = FindContainer<TBind>();

            container.Set(service);
        }
        
        public TBind GetService<TBind>()
        {
            var container = FindContainer<TBind>();

            return container.Get();
        }

        private Container<T> FindContainer<T>()
        {
            var typeBind = typeof(T).Name;
            if (_services.TryGetValue(typeBind, out var container)) return container as Container<T>;

            var bindContainer = new Container<T>();
            _services[typeBind] = bindContainer;

            return bindContainer;
        }
        
        private class Container<TBind> : IService
        {
            private TBind _value;

            public void Set(TBind value) => _value = value;

            public TBind Get() => _value;
        }
    }
}
using System;
using System.Reflection;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.Service.Installer
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private ServiceInfo[] services;

        public override void Init(ServiceContainer container)
        {
            MethodInfo setServiceRef;
            foreach (var serviceInfo in services)
            {
                var serviceType = serviceInfo.service.GetType();
                if (serviceInfo.type == InstallType.Class)
                {
                    
                    var methodInfo = typeof(ServiceContainer).GetMethod("SetServiceSelf");
                    setServiceRef = methodInfo?.MakeGenericMethod(serviceType);
                }
                else
                {
                    var interfaces = serviceInfo.service.GetType().GetInterfaces();
                    
                    var methodInfo = typeof(ServiceContainer).GetMethod("SetService");
                    setServiceRef = methodInfo?.MakeGenericMethod(interfaces[^1], serviceType);
                }
                
                setServiceRef?.Invoke(container, new object[] {serviceInfo.service});
            }
        }

        [Serializable]
        public class ServiceInfo
        {
            public MonoBehaviour service;

            public InstallType type;
        }

        public enum InstallType
        {
            Class,
            Interface
        }
        
    }
}
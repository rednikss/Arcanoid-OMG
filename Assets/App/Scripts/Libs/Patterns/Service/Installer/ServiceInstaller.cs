using System;
using System.Reflection;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.Service.Installer
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private ServiceInfo[] services;

        public override void Init(ProjectContext.ProjectContext context)
        {
            MethodInfo setServiceRef;
            foreach (var serviceInfo in services)
            {
                var serviceType = serviceInfo.service.GetType();
                if (serviceInfo.type == InstallType.Class)
                {
                    
                    var methodInfo = typeof(Container.ServiceContainer).GetMethod("SetServiceSelf");
                    setServiceRef = methodInfo?.MakeGenericMethod(serviceType);
                }
                else
                {
                    var interfaces = serviceInfo.service.GetType().GetInterfaces();
                    
                    var methodInfo = typeof(Container.ServiceContainer).GetMethod("SetService");
                    setServiceRef = methodInfo?.MakeGenericMethod(interfaces[^1], serviceType);
                }
                
                setServiceRef?.Invoke(context.GetContainer(), new object[] {serviceInfo.service});
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
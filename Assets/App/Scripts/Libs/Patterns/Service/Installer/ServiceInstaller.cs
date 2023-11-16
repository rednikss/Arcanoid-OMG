using System.Reflection;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.Patterns.Service.Installer
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private MonoBehaviour[] services;

        public override void Init(ProjectContext.ProjectContext context)
        {
            foreach (var service in services)
            {
                var serviceType = service.GetType();
                var interfaces = service.GetType().GetInterfaces();

                MethodInfo setServiceRef;
                if (interfaces.Length > 0)
                {
                    var methodInfo = typeof(Container.ServiceContainer).GetMethod("SetService");
                    setServiceRef = methodInfo.MakeGenericMethod(interfaces[0], serviceType);
                }
                else
                {
                    var methodInfo = typeof(Container.ServiceContainer).GetMethod("SetServiceSelf");
                    setServiceRef = methodInfo.MakeGenericMethod(serviceType);
                }
                
                setServiceRef.Invoke(context.GetContainer(), new object[] {service});
            }
        }
    }
}
using System.Collections.Generic;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ServiceContainer;
using UnityEngine;

namespace App.Scripts.Architecture.ProjectContext
{
    public class ProjectContext : MonoBehaviour
    {
        [SerializeField] private List<MonoInstaller> installers;
        
        private ServiceContainer container;

        private static ProjectContext _instance;
        public static ProjectContext Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = Resources.Load<ProjectContext>("Project Context");
                _instance.Init();
                
                DontDestroyOnLoad(_instance);
                
                return _instance;
            }
            
        }

        private void Init()
        {
            container = new ServiceContainer();
            foreach (var installer in installers) installer.Init(_instance);
        }

        public ServiceContainer GetContainer() => container;
    }
}
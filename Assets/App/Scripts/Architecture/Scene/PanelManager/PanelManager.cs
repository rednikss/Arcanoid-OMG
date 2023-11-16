using System.Collections.Generic;
using App.Scripts.Architecture.Scene.PanelManager.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Architecture.Scene.PanelManager
{
    public class PanelManager : MonoInstaller 
    {
        [SerializeField] private PanelListScriptable scriptable;

        private readonly List<LocalizedPanelInstaller> _panels = new();

        private ProjectContext _context;

        public override void Init(ProjectContext context)
        {
            _context = context;

            SceneManager.sceneUnloaded += (arg0) => DisableAll();
        }

        public TInstaller GetDisabledPanel<TInstaller>() where TInstaller : LocalizedPanelInstaller
        {
            return FindPanel<TInstaller>(false) ?? CreatePanel<TInstaller>();
        }

        public TInstaller GetEnabledPanel<TInstaller>() where TInstaller : LocalizedPanelInstaller
        {
            return FindPanel<TInstaller>(true);
        }

        private TInstaller FindPanel<TInstaller>(bool activeState) where TInstaller : LocalizedPanelInstaller
        {
            foreach (var panel in _panels)
            {
                if (panel.GetType() != typeof(TInstaller) || panel.isActiveAndEnabled != activeState) continue;
                
                return (TInstaller)panel;
            }

            return null;
        }

        private TInstaller CreatePanel<TInstaller>() where TInstaller : LocalizedPanelInstaller
        {
            foreach (var newPanel in scriptable.panelList)
            {
                if (newPanel.GetType() != typeof(TInstaller)) continue;
                
                var installer = Instantiate(newPanel, transform);
                installer.Init(_context);
                installer.gameObject.SetActive(false);
                
                _panels.Add(installer);
                
                return (TInstaller)installer;
            }

            Debug.LogError($"Attempted to create non-existent panel {typeof(TInstaller).Name}!");

            return null;
        }

        private void DisableAll()
        {
            foreach (var installer in _panels)
            {
                installer.gameObject.SetActive(false);
            }
        }
    }
}
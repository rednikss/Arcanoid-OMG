using System.Collections.Generic;
using App.Scripts.Architecture.Scene.PanelManager.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.UI.PanelControllers.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Architecture.Scene.PanelManager
{
    public class PanelManager : MonoInstaller 
    {
        [SerializeField] private PanelListScriptable scriptable;

        private readonly List<LocalizedPanelController> _disabledPanels = new();
        
        private readonly Stack<LocalizedPanelController> _activePanels = new();

        public override void Init(ServiceContainer container)
        {
            SceneManager.sceneUnloaded += (arg0) => DisableAll();
        }

        public TInstaller GetPanel<TInstaller>() where TInstaller : LocalizedPanelController
        {
            return FindPanel<TInstaller>() ?? CreatePanel<TInstaller>();
        }

        private TInstaller FindPanel<TInstaller>() where TInstaller : LocalizedPanelController
        {
            foreach (var panel in _disabledPanels)
            {
                if (panel.GetType() != typeof(TInstaller)) continue;
                
                return (TInstaller)panel;
            }

            return null;
        }

        private TInstaller CreatePanel<TInstaller>() where TInstaller : LocalizedPanelController
        {
            foreach (var newPanel in scriptable.panelList)
            {
                if (newPanel.GetType() != typeof(TInstaller)) continue;
                
                var panelController = Instantiate(newPanel, transform);
                var index = transform.childCount - 2;
                panelController.transform.SetSiblingIndex(index);
                panelController.HidePanelImmediately();
                
                _disabledPanels.Add(panelController);
                
                return (TInstaller)panelController;
            }

            Debug.LogError($"Attempted to create non-existent panel {typeof(TInstaller).Name}!");

            return null;
        }

        public void AddActive(LocalizedPanelController newPanel)
        {
            if (_activePanels.TryPeek(out var panel))
            {
                panel.SetInteractive(false);
            }
            
            _disabledPanels.Remove(newPanel);
            _activePanels.Push(newPanel);
            _activePanels.Peek().SetInteractive(true);
        }

        public LocalizedPanelController RemoveActive()
        {
            var activePanel = _activePanels.Pop();
            _disabledPanels.Add(activePanel);
            activePanel.SetInteractive(false);
            
            if (_activePanels.TryPeek(out var panel))
            {
                panel.SetInteractive(true);
            }
            
            return activePanel;
        }
        
        private void DisableAll()
        {
            while (_activePanels.Count > 0) RemoveActive().HidePanelImmediately();
        }
    }
}
using System.Collections.Generic;
using App.Scripts.Architecture.PanelManager.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.PanelManager
{
    public class PanelManager : MonoInstaller
    {
        [SerializeField] private PanelListScriptable scriptable;

        [SerializeField] private string initPanelName;
        
        private readonly List<PanelInfo> _panels = new();

        private ProjectContext.ProjectContext _context;

        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetContainer().SetServiceSelf(this);
            _context = context;
            
            if (initPanelName.Length == 0) return;
            
            InstallPanel(initPanelName);
        }
        
        public void InstallPanel(string panelName)
        {
            FindPanel(panelName, false, true).installer.gameObject.SetActive(true);
        }

        public void DisablePanel(string panelName)
        {
            FindPanel(panelName, true, false).installer.gameObject.SetActive(false);
        }

        private PanelInfo FindPanel(string panelName, bool isActiveState, bool createIfNotFound)
        {
            foreach (var panel in _panels)
            {
                if (!panel.name.Equals(panelName) || panel.installer.isActiveAndEnabled != isActiveState) continue;
                
                return panel;
            }

            return createIfNotFound ? CreatePanel(panelName) : null;
        }

        private PanelInfo CreatePanel(string panelName)
        {
            foreach (var newPanel in scriptable.panelList)
            {
                if (!newPanel.name.Equals(panelName)) continue;
                
                var newInstaller = Instantiate(newPanel.installer, transform);
                newInstaller.Init(_context);

                var info = new PanelInfo(newPanel.name, newInstaller);
                _panels.Add(info);
                
                return info;
            }

            Debug.LogError($"Attempted to create non-existent panel {panelName}!");

            return null;
        }
    }
}
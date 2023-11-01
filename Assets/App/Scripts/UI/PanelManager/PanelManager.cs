using System.Collections.Generic;
using App.Scripts.Architecture.InitPoint.MonoInitializable;
using App.Scripts.UI.PanelManager.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.PanelManager
{
    public class PanelManager : MonoInitializable
    {
        [SerializeField] private PanelListScriptable scriptable;

        private List<PanelListScriptable.PanelInfo> _panels = new();

        [SerializeField] private string firstPanelName;
        
        public override void Init()
        {
            if (firstPanelName.Length == 0) return;
            
            InstallPanel(firstPanelName);
        }
        
        public void InstallPanel(string panelName)
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                var panel = _panels[i];
                if (panel.name.Equals(panelName) && !panel.installer.isActiveAndEnabled)
                {
                    panel.installer.gameObject.SetActive(true);
                    return;
                }
            }

            for (int i = 0; i < scriptable.panelList.Length; i++)
            {
                var newPanel = scriptable.panelList[i];
                if (newPanel.name.Equals(panelName))
                {
                    var newInstaller = Instantiate(newPanel.installer, transform);
                    _panels.Add(new PanelListScriptable.PanelInfo(newPanel.name, newInstaller));
                    newInstaller.SetupPanel();
                }
            }
        }

        public void DisablePanel(string panelName)
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                var panel = _panels[i];
                if (panel.name.Equals(panelName) && panel.installer.isActiveAndEnabled)
                {
                    panel.installer.gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
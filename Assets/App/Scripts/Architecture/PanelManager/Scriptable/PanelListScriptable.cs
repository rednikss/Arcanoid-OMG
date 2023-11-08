using System;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;

namespace App.Scripts.Architecture.PanelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Panel List", menuName = "Scriptable Object/Panel Config", order = 0)]
    public class PanelListScriptable : ScriptableObject
    {
        public PanelInfo[] panelList;
    }
    
    [Serializable]
    public class PanelInfo
    {
        public PanelInfo(string name, LocalizedPanelInstaller installer)
        {
            this.name = name;
            this.installer = installer;
        }

        public string name;

        public LocalizedPanelInstaller installer;
    }
}
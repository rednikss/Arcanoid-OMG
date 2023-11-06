using System;
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.UI.PanelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Panel List", menuName = "Scriptable Object/Panel Config", order = 0)]
    public class PanelListScriptable : ScriptableObject
    {
        public PanelInfo[] panelList;
    }
    
    [Serializable]
    public class PanelInfo
    {
        public PanelInfo(string name, MonoInstaller installer)
        {
            this.name = name;
            this.installer = installer;
        }

        public string name;

        public MonoInstaller installer;
    }
}
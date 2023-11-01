using System;
using App.Scripts.UI.PanelManager.InstallerBase;
using UnityEngine;

namespace App.Scripts.UI.PanelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Panel List", menuName = "Scriptable Object/Panel Config", order = 0)]
    public class PanelListScriptable : ScriptableObject
    {
        [Serializable]
        public class PanelInfo
        {
            public PanelInfo(string name, PanelInstaller installer)
            {
                this.name = name;
                this.installer = installer;
            }

            public string name;

            public PanelInstaller installer;
        }

        public PanelInfo[] panelList;
    }
}
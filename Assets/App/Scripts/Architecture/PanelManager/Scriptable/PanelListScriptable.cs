using App.Scripts.UI.PanelInstallers.Base;
using UnityEngine;

namespace App.Scripts.Architecture.PanelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Panel List", menuName = "Scriptable Object/Base/Panel Config", order = 0)]
    public class PanelListScriptable : ScriptableObject
    {
        public LocalizedPanelInstaller[] panelList;
    }
}
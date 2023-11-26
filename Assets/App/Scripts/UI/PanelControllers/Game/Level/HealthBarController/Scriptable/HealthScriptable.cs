using UnityEngine;

namespace App.Scripts.UI.PanelControllers.Game.Level.HealthBarController.Scriptable
{
    [CreateAssetMenu(fileName = "Health Options", menuName = "Scriptable Object/Level/Health Config", order = 0)]
    public class HealthScriptable : ScriptableObject
    {
        [Min(1)] public int healthCount;
    }
}
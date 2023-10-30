using UnityEngine;

namespace App.Scripts.Architecture.OptionsInstaller.Scriptable
{
    [CreateAssetMenu(fileName = "Base Options", menuName = "Scriptable Object/Base/Base Config", order = 0)]
    public class BaseOptionsScriptable : ScriptableObject
    {
        [Min(0)] public int targetFPS;
    }
}
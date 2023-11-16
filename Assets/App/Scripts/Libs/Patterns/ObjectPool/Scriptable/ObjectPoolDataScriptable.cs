using UnityEngine;

namespace App.Scripts.Libs.Patterns.ObjectPool.Scriptable
{
    [CreateAssetMenu(fileName = "Object Pool Options", menuName = "Scriptable Object/Base/Object Pool Config", order = 0)]
    public class ObjectPoolDataScriptable : ScriptableObject
    {
        [Min(0)] public int defaultSize;
        
        [Min(0)] public int maximumSize;
    }
}
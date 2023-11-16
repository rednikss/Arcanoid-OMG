using UnityEngine;

namespace App.Scripts.Libs.Utilities.Camera.Collider.Scriptable
{
    [CreateAssetMenu(fileName = "Paddings", menuName = "Scriptable Object/Base/Paddings Config", order = 0)]
    public class PaddingScriptable : ScriptableObject
    {
        [Range(-1, 1)] public float right;
        [Range(-1, 1)] public float left;
        [Range(-1, 1)] public float top;
        [Range(-1, 1)] public float bottom;
    }
}
using App.Scripts.Libs.Utilities.Camera.Collider.Scriptable;
using UnityEngine;

namespace App.Scripts.Game.LevelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Level Size", menuName = "Scriptable Object/Level/Size Config", order = 0)]
    public class LevelSizeScriptable : ScriptableObject
    {
        public PaddingScriptable padding;

        [Header("Unit Spacing")]
        [Range(0, 0.5f)] public float row;
        [Range(0, 0.5f)] public float column;

    }
}
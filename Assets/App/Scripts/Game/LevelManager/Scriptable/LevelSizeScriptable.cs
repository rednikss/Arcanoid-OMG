using UnityEngine;

namespace App.Scripts.Game.LevelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Level Size", menuName = "Scriptable Object/Level/Size Config", order = 0)]
    public class LevelSizeScriptable : ScriptableObject
    {
        [Header("Percent Padding")]
        [Range(0, 1)] public float top;
        [Range(0, 1)] public float right;
        [Range(0, 1)] public float left;

        [Header("Unit Spacing")]
        [Range(0, 0.5f)] public float row;
        [Range(0, 0.5f)] public float column;

    }
}
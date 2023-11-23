using UnityEngine;

namespace App.Scripts.Game.LevelManager.DifficultyIncreaser.Scriptables.SpeedRange
{
    [CreateAssetMenu(fileName = "Speed Options", menuName = "Scriptable Object/Level/Speed Config", order = 0)]
    public class SpeedRangeScriptable : ScriptableObject
    {
        [Min(1)] public float minSpeed;
        
        [Min(1)] public float maxSpeed;
    }
}
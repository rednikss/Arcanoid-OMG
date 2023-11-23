using System;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Ball.DirectionCorrector.Scriptable
{
    [CreateAssetMenu(fileName = "Direction Options", menuName = "Scriptable Object/Level/Direction Options", order = 0)]
    public class DirectionCorrectionScriptable : ScriptableObject
    {
        public DirectionZone[] directions;
    }
    
    [Serializable]
    public class DirectionZone
    {
        public Vector2 Direction;
        public int Width;

        public DirectionZone(Vector2 direction, int width)
        {
            Direction = direction;
            Width = width;
        }
    }
}
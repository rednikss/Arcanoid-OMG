using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks.Base.Scriptable
{
    [CreateAssetMenu(fileName = "Block", menuName = "Scriptable Object/Block/Block Data", order = 0)]
    public class BlockDataScriptable : ScriptableObject
    {
        public int blockID;

        [Min(-1)] public int health;
    }
}
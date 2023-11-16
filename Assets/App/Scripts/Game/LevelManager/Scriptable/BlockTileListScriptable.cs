using App.Scripts.Game.Mechanics.Blocks.Base;
using UnityEngine;

namespace App.Scripts.Game.LevelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Tiles List", menuName = "Scriptable Object/Level/Tiles Config", order = 0)]
    public class BlockTileListScriptable : ScriptableObject
    {
        public Block[] blocks;

        public Block GetByBlockID(int blockId)
        {
            foreach (var block in blocks)
            {
                if (block.scriptable.blockID == blockId) return block;
            }
            
            Debug.LogError($"Attempted to get a block with invalid id {blockId}!");
            return null;
        }
    }
}
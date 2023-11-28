using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.GameObjects.Blocks.Base
{
    [CreateAssetMenu(fileName = "Block Tile", menuName = "Scriptable Object/Level/Block Tile")]
    public class BlockTile : Tile
    {
        [SerializeField] [Min(0)] private int id;
        public int ID => id;
    }
}
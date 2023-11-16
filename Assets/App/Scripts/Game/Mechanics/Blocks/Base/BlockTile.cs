using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.Mechanics.Blocks.Base
{
    [CreateAssetMenu(fileName = "Block Tile", menuName = "Scriptable Object/Level/Block Tile")]
    public class BlockTile : Tile
    {
        [SerializeField] private int id;
        public int ID => id;
    }
}
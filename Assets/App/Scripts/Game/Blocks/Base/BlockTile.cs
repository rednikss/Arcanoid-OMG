using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.Blocks.Base
{
    [CreateAssetMenu(fileName = "Block Tile", menuName = "Block Tile")]
    public class BlockTile : Tile
    {
        [SerializeField] private int id;
        public int ID
        {
            get => id;
        }
        
        [SerializeField] private int health;
    }
}
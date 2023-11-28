using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.GameObjects.Boost.Base
{
    [CreateAssetMenu(fileName = "Boost Tile", menuName = "Scriptable Object/Level/Boost Tile")]
    public class BoostTile : Tile
    {
        [SerializeField] [Min(0)] private int id;
        public int ID => id;
    }
}
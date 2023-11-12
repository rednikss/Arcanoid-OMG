using App.Scripts.Game.Blocks.Base;
using UnityEngine;

namespace App.Scripts.Game.LevelManager.Scriptable
{
    [CreateAssetMenu(fileName = "Tiles List", menuName = "Scriptable Object/Level/Tiles Config", order = 0)]
    public class BlockTileListScriptable : ScriptableObject
    {
        public BlockTile[] tiles;
    }
}
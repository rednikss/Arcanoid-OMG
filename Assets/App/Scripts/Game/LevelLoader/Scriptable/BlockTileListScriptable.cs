using App.Scripts.Game.Blocks.Base;
using UnityEngine;

namespace App.Scripts.Game.LevelLoader.Scriptable
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class BlockTileListScriptable : ScriptableObject
    {
        public BlockTile[] BlockTiles;
    }
}
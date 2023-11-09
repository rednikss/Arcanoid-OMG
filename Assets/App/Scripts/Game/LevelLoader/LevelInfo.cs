using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.LevelLoader
{
    [Serializable]
    public class LevelInfo
    {
        public int HorizontalSize;

        public List<BlockInfo> Blocks;

        public LevelInfo(int horizontalSize)
        {
            HorizontalSize = horizontalSize;
            Blocks = new List<BlockInfo>();
        }
    }
    
    [Serializable]
    public class BlockInfo
    {
        public int BlockID;
        public Vector3Int Pos;

        public BlockInfo(int blockID, Vector3Int pos)
        {
            BlockID = blockID;
            Pos = pos;
        }
    }
}
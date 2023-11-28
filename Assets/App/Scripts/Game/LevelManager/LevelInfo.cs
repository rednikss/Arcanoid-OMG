using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Game.LevelManager
{
    [Serializable]
    public class LevelInfo
    {
        public Vector3Int size;

        public List<BlockInfo> blocks;
        
        public LevelInfo()
        {
            size = Vector3Int.zero;
            blocks = new List<BlockInfo>();
        }
        
        public LevelInfo(Vector3Int size)
        {
            this.size = size;
            blocks = new List<BlockInfo>();
        }
    }
    
    [Serializable]
    public class BlockInfo
    {
        public int id;
        public Vector3Int pos;
        public int boostID;

        public BlockInfo(int id, Vector3Int pos, int boostID = -1)
        {
            this.id = id;
            this.pos = pos;
            this.boostID = boostID;
        }
    }
}
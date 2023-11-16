using System.IO;
using App.Scripts.Game.LevelManager.Scriptable;
using App.Scripts.Game.Mechanics.Blocks.Base;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.Camera.Adapter;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.LevelManager
{
    public class LevelLoader : MonoInstaller
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private LevelSizeScriptable mapSize;
        [SerializeField] private BlockTileListScriptable blocks;

        private CameraAdapter adapter;

        private LevelInfo currentLevelInfo;
        private float levelSize;
        
        public override void Init(ProjectContext context)
        {
            adapter = context.GetContainer().GetService<CameraAdapter>();

            Vector3 percentPosition = new(mapSize.padding.right, 1 - mapSize.padding.top, 0);
            tilemap.transform.position = adapter.PercentToWorld(percentPosition) + Vector3.up * mapSize.row;

            LoadLevel();
        }

        public void LoadLevel()
        {
            currentLevelInfo = JsonUtility.FromJson<LevelInfo>(Resources.Load<TextAsset>("Data/Levels/Level").text);
            
            InitTilemap();
            InitBlocks();
        }
        
        private void InitTilemap()
        {
            levelSize = (1 - mapSize.padding.right - mapSize.padding.left) * 2 * adapter.GetSize().x;
            levelSize -= (currentLevelInfo.size.x - 1) * mapSize.column;
            levelSize /= currentLevelInfo.size.x;

            tilemap.transform.localScale = Vector3.one * levelSize;
            tilemap.layoutGrid.cellGap = new Vector3(mapSize.column, mapSize.row, 0) / levelSize;
        }
        
        public void InitBlocks()
        {
            foreach (var blockInfo in currentLevelInfo.blocks)
            {
                var newBlock = Instantiate(blocks.GetByBlockID(blockInfo.id), transform);

                newBlock.transform.position = tilemap.GetCellCenterWorld(blockInfo.pos);
                newBlock.transform.localScale *= levelSize;
            }
        }
        
#if UNITY_EDITOR
        
        public void SaveCurrentAsLevel(string filepath)
        {
            tilemap.CompressBounds();
            var cellBounds = tilemap.cellBounds;
            var positions = cellBounds.allPositionsWithin;
            
            Vector3Int topLeftPosition = cellBounds.position + new Vector3Int(0, cellBounds.size.y, 0);

            LevelInfo level = new LevelInfo(cellBounds.size);
            foreach (var position in positions)
            {
                var block = tilemap.GetTile<BlockTile>(position);
                
                if (block == null) continue;
                
                level.blocks.Add(new BlockInfo(block.ID, position - topLeftPosition));
            }
            
            var stream = File.Create(filepath);
            StreamWriter writer = new(stream);
            
            writer.Write(JsonUtility.ToJson(level));
            
            writer.Close();
            stream.Close();
        }
#endif
    }
}
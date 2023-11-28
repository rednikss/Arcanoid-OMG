using System.IO;
using App.Scripts.Architecture.Scene.Packs.StateController;
using App.Scripts.Game.GameObjects.Blocks.Base;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.GameObjects.Boost.Base;
using App.Scripts.Game.LevelManager.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Camera.Adapter;
using App.Scripts.Libs.Utilities.Data.DataProvider;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.LevelManager
{
    public class LevelLoader : MonoInstaller
    {
        [SerializeField] private Tilemap blockTilemap;
        [SerializeField] private Tilemap boostTilemap;
        [SerializeField] private LevelSizeScriptable mapSize;

        [SerializeField] private string packStatePath;
        [SerializeField] private string levelPath;
        
        private CameraAdapter adapter;
        private BlockPool pool;
        private FileDataProvider fileProvider;
        private ResourcesTextDataProvider resourcesProvider;
        
        private PackStateInfo packState;
        private LevelInfo currentLevel;
        private float levelSize;
        
        public override void Init(ServiceContainer container)
        {
            pool = container.GetService<BlockPool>();
            adapter = container.GetService<CameraAdapter>();
            resourcesProvider = container.GetService<ResourcesTextDataProvider>();
            fileProvider = container.GetService<FileDataProvider>();

            Vector3 percentPosition = new(mapSize.padding.right, 1 - mapSize.padding.top, 0);
            blockTilemap.transform.position = boostTilemap.transform.position =
                adapter.PercentToWorld(percentPosition) + Vector3.up * mapSize.row;
        }

        public void LoadLevel()
        {
            packState = fileProvider.LoadData<PackStateInfo>(nameof(PackStateInfo), packStatePath);
            currentLevel = resourcesProvider.LoadData<LevelInfo>(packState.LevelToLoad.ToString(), levelPath);
            
            InitTilemap();
            InitBlocks();
        }

        public int GetLevelBlockCount() => currentLevel.blocks.Count;

        private void InitTilemap()
        {
            levelSize = (1 - mapSize.padding.right - mapSize.padding.left) * 2 * adapter.GetSize().x;
            levelSize -= (currentLevel.size.x - 1) * mapSize.column;
            levelSize /= currentLevel.size.x;

            blockTilemap.transform.localScale = Vector3.one * levelSize;
            blockTilemap.layoutGrid.cellGap = boostTilemap.layoutGrid.cellGap = 
                new Vector3(mapSize.column, mapSize.row, 0) / levelSize;
        }

        private void InitBlocks()
        {
            foreach (var blockInfo in currentLevel.blocks)
            {
                var newBlock = pool.Get(blockInfo.id);
                newBlock.SetBoost(blockInfo.boostID);
                
                var blockTransform = newBlock.transform;
                
                blockTransform.position = blockTilemap.GetCellCenterWorld(blockInfo.pos);
                blockTransform.localScale *= levelSize;
            }
        }
        
#if UNITY_EDITOR
        
        public void SaveCurrentAsLevel(string filepath)
        {
            blockTilemap.CompressBounds();
            var cellBounds = blockTilemap.cellBounds;
            var positions = cellBounds.allPositionsWithin;
            
            Vector3Int topLeftPosition = cellBounds.position + new Vector3Int(0, cellBounds.size.y, 0);

            LevelInfo level = new LevelInfo(cellBounds.size);
            foreach (var position in positions)
            {
                var block = blockTilemap.GetTile<BlockTile>(position);
                
                if (block == null) continue;

                var boost = boostTilemap.GetTile<BoostTile>(position);

                level.blocks.Add(boost == null
                    ? new BlockInfo(block.ID, position - topLeftPosition)
                    : new BlockInfo(block.ID, position - topLeftPosition, boost.ID));
            }
            
            
            using var stream = File.Create(filepath);
            using StreamWriter writer = new(stream);
            
            writer.Write(JsonUtility.ToJson(level, true));
            
            blockTilemap.ClearAllTiles();
            boostTilemap.ClearAllTiles();
        }
#endif
    }
}
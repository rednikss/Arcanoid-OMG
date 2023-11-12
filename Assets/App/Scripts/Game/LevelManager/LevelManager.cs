using System.IO;
using App.Scripts.Game.Blocks.Base;
using App.Scripts.Game.LevelManager.Scriptable;
using App.Scripts.Libs.CameraUtilities.Adapter;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.LevelManager
{
    public class LevelManager : MonoInstaller
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private LevelSizeScriptable mapSize;
        [SerializeField] private BlockTileListScriptable blocks;

        private CameraAdapter adapter;
        
        public override void Init(ProjectContext context)
        {
            adapter = context.GetContainer().GetService<CameraAdapter>();

            Vector2 percentPosition = new(mapSize.right, 1 - mapSize.top);

            LevelInfo info = JsonUtility.FromJson<LevelInfo>(Resources.Load<TextAsset>("Levels/Level").text);
            
            float size = (1 - mapSize.right - mapSize.left) * 2 * adapter.GetSize().x;
            size -= (info.size.x - 1) * mapSize.column;
            size /= info.size.x;
            
            tilemap.transform.localScale = Vector3.one * size;
            tilemap.layoutGrid.cellGap = new Vector2(mapSize.column, mapSize.row) / size;
            tilemap.transform.position = adapter.PercentToWorld(percentPosition) + Vector2.up * mapSize.row;

            foreach (var blockInfo in info.blocks)
            {
                tilemap.SetTile(blockInfo.pos, blocks.tiles[blockInfo.id]);
            }
            
            tilemap.CompressBounds();
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
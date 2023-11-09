using System.IO;
using App.Scripts.Architecture.CameraUtilities.Adapter;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Game.Blocks.Base;
using App.Scripts.Game.LevelManager.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.LevelManager
{
    public class LevelManager : MonoInstaller
    {
        [SerializeField] private BlockTileListScriptable scriptable;
        [SerializeField] private Tilemap tilemap;

        [Header("Percent Padding")]
        [SerializeField] [Range(0, 1)] private float top;
        [SerializeField] [Range(0, 1)] private float right;
        [SerializeField] [Range(0, 1)] private float left;

        [Header("World Spacing")]
        [SerializeField] [Range(0, 1)] private float row;
        [SerializeField] [Range(0, 1)] private float column;
        
        private CameraAdapter adapter;
        
        public override void Init(ProjectContext context)
        {
            adapter = context.GetContainer().GetService<CameraAdapter>();

            Vector2 percentPosition = new Vector2(right, 1 - top);

            LevelInfo info = JsonUtility.FromJson<LevelInfo>(new StreamReader(
                File.OpenRead(Path.Combine(Application.dataPath, "App", "Data", "Levels", "Level.txt"))
                ).ReadToEnd());
            
            float size = (1 - right - left) * 2 * adapter.GetHorizontalSize();
            size -= (info.size.x - 1) * column;
            size /= info.size.x;
            
            tilemap.transform.localScale = Vector3.one * size;
            tilemap.layoutGrid.cellGap = new Vector3(column, row, 0) / size;
            tilemap.transform.position = adapter.PercentToWorld(percentPosition);

            foreach (var blockInfo in info.blocks)
            {
                tilemap.SetTile(blockInfo.pos, scriptable.blockTiles[blockInfo.id]);
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
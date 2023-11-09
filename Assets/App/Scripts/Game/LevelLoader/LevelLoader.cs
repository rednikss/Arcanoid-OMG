using System.IO;
using App.Scripts.Architecture.CameraUtilities.Adapter;
using App.Scripts.Architecture.ProjectContext;
using App.Scripts.Game.Blocks.Base;
using App.Scripts.Game.LevelLoader.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Scripts.Game.LevelLoader
{
    public class LevelLoader : MonoInstaller
    {
        [SerializeField] private BlockTileListScriptable scriptable;
        [SerializeField] private Tilemap tilemap;

        [Header("Percent Padding")]
        [SerializeField] [Range(0, 1)] private float top;
        //[SerializeField] [Range(0, 1)] private float bottom;
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

            
            float xSize = ((1 - right - left) * 2 * adapter.GetHorizontalSize()
                           - (info.HorizontalSize - 1) * column) / info.HorizontalSize;
            
            tilemap.gameObject.transform.localScale = Vector3.one * xSize;
            tilemap.transform.position = adapter.PercentToWorld(percentPosition);
            tilemap.layoutGrid.cellGap = new Vector3(column, row, 0) / xSize;

            foreach (var blockInfo in info.Blocks)
            {
                tilemap.SetTile(blockInfo.Pos, scriptable.BlockTiles[blockInfo.BlockID]);
            }
            
            tilemap.CompressBounds();
        }
        
#if UNITY_EDITOR
        public void SaveCurrentAsLevel(string filepath)
        {
            tilemap.CompressBounds();
            var cellBounds = tilemap.cellBounds;
            var positions = cellBounds.allPositionsWithin;
            
            Vector3Int delta = cellBounds.position + new Vector3Int(0, cellBounds.size.y, 0);

            LevelInfo level = new LevelInfo(cellBounds.size.x);
            foreach (var position in positions)
            {
                var block = tilemap.GetTile<BlockTile>(position);
                
                if (block == null) continue;
                
                level.Blocks.Add(new BlockInfo(block.ID, position - delta));
            }
            
            var stream = File.Create($"{Path.Combine(Application.dataPath, filepath, "Level")}.txt");
            StreamWriter writer = new(stream);
            
            writer.Write(JsonUtility.ToJson(level));
            
            writer.Close();
            stream.Close();
        }
#endif
    }
}
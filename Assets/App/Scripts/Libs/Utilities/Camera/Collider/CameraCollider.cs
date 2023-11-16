using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Utilities.Camera.Collider.Scriptable;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Camera.Collider
{
    public class CameraCollider : MonoInstaller
    {
        [SerializeField] private PaddingScriptable pad;
        [SerializeField] private EdgeCollider2D edgeCollider;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            var adapter = context.GetContainer().GetService<Adapter.CameraAdapter>();

            var offsetMagnitude = edgeCollider.edgeRadius;
            
            edgeCollider.points = new[]
            {
                adapter.PercentToWorld(new Vector2(pad.left, pad.bottom)) 
                - Vector2.one * offsetMagnitude,
                
                adapter.PercentToWorld(new Vector2(pad.left, 1 - pad.top)) 
                + new Vector2(-1, 1) * offsetMagnitude,
                
                adapter.PercentToWorld(new Vector2(1 - pad.right, 1 - pad.top)) 
                + Vector2.one * offsetMagnitude,
                
                adapter.PercentToWorld(new Vector2(1 - pad.right, pad.bottom)) 
                + new Vector2(1, -1) * offsetMagnitude,
                
                adapter.PercentToWorld(new Vector2(pad.left, pad.bottom))
                - Vector2.one * offsetMagnitude,
            };
        }
    }
}
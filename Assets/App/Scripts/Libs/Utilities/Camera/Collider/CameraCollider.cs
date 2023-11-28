using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Camera.Collider.Scriptable;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Camera.Collider
{
    public class CameraCollider : MonoInstaller
    {
        [SerializeField] private PaddingScriptable pad;
        [SerializeField] private EdgeCollider2D edgeCollider;
        
        public override void Init(ServiceContainer container)
        {
            var adapter = container.GetService<Adapter.CameraAdapter>();

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
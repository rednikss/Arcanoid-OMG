using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.CameraUtilities.Collider
{
    public class CameraCollider : MonoInstaller
    {
        [SerializeField] private EdgeCollider2D edgeCollider;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            var adapter = context.GetContainer().GetService<Adapter.CameraAdapter>();

            edgeCollider.edgeRadius = 0;
            edgeCollider.points = new[]
            {
                adapter.PercentToWorld(Vector2.zero),
                adapter.PercentToWorld(Vector2.up),
                adapter.PercentToWorld(Vector2.one),
                adapter.PercentToWorld(Vector2.right),
                adapter.PercentToWorld(Vector2.zero)
            };
        }
    }
}
using App.Scripts.Libs.CameraUtilities.Adapter;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.StateMachine.MonoSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Game.Platform
{
    public class InputFollower : MonoSystem
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] [Min(0)] private float speed;
        
        private CameraAdapter adapter;

        public override void Init(ProjectContext context)
        {
            adapter = context.GetContainer().GetService<CameraAdapter>();
        }

        public override void UpdateWithDT(float dt)
        {
            if (!Input.GetMouseButton(0) || EventSystem.current.IsPointerOverGameObject())
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            
            var targetPos = adapter.PixelToWorld(Input.mousePosition);

            float velocity = Mathf.Clamp((targetPos - transform.position).x, -1, 1);
            velocity *= speed;
            
            _rigidbody.velocity = Vector2.right * velocity;
        }
    }
}
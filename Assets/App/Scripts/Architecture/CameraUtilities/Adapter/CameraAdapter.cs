using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.CameraUtilities.Adapter
{
    public class CameraAdapter : MonoInstaller
    {
        [SerializeField] private Camera currentCamera;
        
        private float _verticalSize;

        private float _screenAspect;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetContainer().SetServiceSelf(this);
            
            _verticalSize = currentCamera.orthographicSize;
            _screenAspect = currentCamera.aspect;
        }

        public Vector2 PercentToWorld(Vector2 percentPosition)
        {
            percentPosition *= 2 * _verticalSize;
            percentPosition.x *= _screenAspect;

            percentPosition.x -= _verticalSize * _screenAspect;
            percentPosition.y -= _verticalSize;

            return percentPosition + (Vector2) currentCamera.transform.position;
        }

        public Vector3 PixelToWorld(Vector3 position)
        {
            return currentCamera.ScreenToWorldPoint(position);
        }
        
        public float GetVerticalSize() => _verticalSize;
        
        public float GetHorizontalSize() => _verticalSize * _screenAspect;
        
        public float GetAspect() => _screenAspect;
    }
}
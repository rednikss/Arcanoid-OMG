using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Libs.Utilities.Camera.Adapter
{
    public class CameraAdapter : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera currentCamera;
        
        private Vector2 _unitSize;

        private float _cameraAspect;
        
        public override void Init(ServiceContainer container)
        {            
            _cameraAspect = currentCamera.aspect;
            
            _unitSize = Vector2.one * currentCamera.orthographicSize;
            _unitSize.x *= _cameraAspect;
        }

        public Vector2 PercentToWorld(Vector2 percentPosition)
        {
            percentPosition.Scale(_unitSize * 2);
            percentPosition -= _unitSize;

            return percentPosition + (Vector2) currentCamera.transform.position;
        }
        
        public Vector3 PercentToWorld(Vector3 percentPosition)
        {
            return PercentToWorld((Vector2) percentPosition);
        }

        public Vector3 PixelToWorld(Vector3 position)
        {
            return currentCamera.ScreenToWorldPoint(position);
        }

        public Vector2 PixelToWorld(Vector2 position)
        {
            return currentCamera.ScreenToWorldPoint(position);
        }
        
        public Vector2 GetSize() => _unitSize;
    }
}
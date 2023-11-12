using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Libs.CameraUtilities.Adapter
{
    public class CameraAdapter : MonoInstaller
    {
        [SerializeField] private Camera currentCamera;
        
        private Vector2 _unitSize;

        private float _cameraAspect;
        
        public override void Init(ProjectContext.ProjectContext context)
        {
            context.GetContainer().SetServiceSelf(this);
            
            _cameraAspect = currentCamera.aspect;
            
            _unitSize = Vector2.one * currentCamera.orthographicSize;
            _unitSize.x *= _cameraAspect;
        }

        public Vector2 PercentToWorld(Vector2 percentPosition)
        {
            percentPosition *= 2 * _unitSize;
            percentPosition -= _unitSize;

            return percentPosition + (Vector2) currentCamera.transform.position;
        }

        public Vector3 PixelToWorld(Vector3 position)
        {
            return currentCamera.ScreenToWorldPoint(position);
        }
        
        public Vector2 GetSize() => _unitSize;

        public float GetAspect() => _cameraAspect;
    }
}
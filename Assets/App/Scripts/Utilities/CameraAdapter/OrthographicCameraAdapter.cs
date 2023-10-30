using App.Scripts.Architecture.MonoInitializable;
using UnityEngine;

namespace App.Scripts.Utilities.CameraAdapter
{
    public class OrthographicCameraAdapter : MonoInitializable
    {
        [SerializeField] private Camera currentCamera;
        
        private float _verticalSize;

        private float _screenAspect;
        
        public override void Init()
        {
            _verticalSize = currentCamera.orthographicSize;
            _screenAspect = currentCamera.aspect;
        }

        public void GetAdaptedPositionByPercent(ref Vector3 percentPosition)
        {
            percentPosition *= _verticalSize;
            percentPosition.x *= _screenAspect;
        }

        public Vector3 AdaptUnitPosition(Vector3 position)
        {
            position.x *= _screenAspect;
            return position;
        }

        public Vector3 AdaptPixelPosition(Vector3 position)
        {
            return currentCamera.ScreenToWorldPoint(position);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos() 
        {
            Init();
        }
#endif
    }
}
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using App.Scripts.Libs.Utilities.Camera.Adapter;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Architecture.Project.InputObserver
{
    public class InputObserver : MonoInstaller
    {
        private Vector3 inputPixelPosition => Input.mousePosition;
        
        public bool IsPressed => Input.GetMouseButton(0);
        
        public bool IsDown => Input.GetMouseButtonDown(0);
        
        public bool IsUp => Input.GetMouseButtonUp(0);

        public bool IsOverGameObject => EventSystem.current.IsPointerOverGameObject();

        public bool IsPressedInGame => IsPressed && !IsOverGameObject;
        
        public bool IsDownInGame => IsDown && !IsOverGameObject;
        
        public bool IsUpInGame => IsUp && !IsOverGameObject;
        
        
        private CameraAdapter _adapter;
        
        public override void Init(ProjectContext context)
        {
            _adapter = context.GetContainer().GetService<CameraAdapter>();
        }

        public Vector3 GetPixelPosition() => inputPixelPosition;

        public Vector3 GetWorldPosition() => _adapter.PixelToWorld(inputPixelPosition);

    }
}
using System.Threading.Tasks;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.Utilities.Camera.Adapter;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Move
{
    public class AnimatedCanvasMoveView : AnimatedCanvasGroupView
    {
        [SerializeField] private Vector2 showDirection;
        
        private CameraAdapter _adapter;
        
        private Transform _canvasTransform;
        
        private Vector3 _openedPos;
        private Vector3 _closedPos;

        public override void Init(ServiceContainer container)
        {
            _adapter = container.GetService<CameraAdapter>();
            
            _canvasTransform = canvasGroup.transform;

            Vector3 deltaPos = showDirection.normalized * 2 * _adapter.GetSize();
            
            _openedPos = _closedPos = (Vector2)_canvasTransform.position;
            _closedPos -= deltaPos;
        }

        public override async Task Show()
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();

            _canvasTransform.position = _closedPos;
            await _canvasTransform.DOMove(_openedPos, scriptable.animationTime)
                .SetEase(scriptable.showEase)
                .SetLink(gameObject)
                .AsyncWaitForCompletion();
        }

        public override async Task Hide()
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();

            _canvasTransform.position = _openedPos;
            await _canvasTransform.DOMove(_closedPos, scriptable.animationTime)
                .SetEase(scriptable.hideEase)
                .SetLink(gameObject)
                .AsyncWaitForCompletion();
        }

        
        public override void ImmediateEnable()
        {
            base.ImmediateEnable();
            
            _canvasTransform.position = _openedPos;
        }
        
        public override void ImmediateDisable()
        {
            base.ImmediateDisable();
            
            _canvasTransform.position = _closedPos;
        }
    }
}

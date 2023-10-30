using System;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base;
using App.Scripts.Utilities.CameraAdapter;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Move
{
    public class AnimatedCanvasMoveView : CanvasGroupView
    {
        [Header("Additional Options")]
        [SerializeField] private Vector2 showDirection;
        
        [SerializeField] private Canvas parentCanvas;
        
        [SerializeField] private OrthographicCameraAdapter adapter;
        
        private Transform _canvasTransform;
        
        private Vector2 _openedPos;
        private Vector2 _closedPos;

        public override void Init()
        {
            _canvasTransform = canvasGroup.transform;
            _openedPos = _closedPos = _canvasTransform.position;
            _closedPos -= showDirection.normalized * 2 * adapter.AdaptPixelPosition(parentCanvas.pixelRect.size);
            canvasGroup.interactable = false;
        }

        public override void Show(Action onComplete = null)
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();
            
            canvasGroup.interactable = false;
            _canvasTransform.DOMove(_openedPos, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject)
                .SetEase(scriptable.showEase)
                .OnStart(() => canvasGroup.gameObject.SetActive(true))
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    onComplete?.Invoke();
                });
        }

        public override void Hide(Action onComplete = null)
        {
            if (DOTween.IsTweening(canvasGroup)) canvasGroup.DOKill();
            
            canvasGroup.interactable = false;
            _canvasTransform.DOMove(_closedPos, scriptable.animationTime)
                .SetUpdate(true)
                .SetLink(gameObject)
                .SetEase(scriptable.hideEase)
                .OnStart(() =>
                {
                    canvasGroup.gameObject.SetActive(true);
                    _canvasTransform.position = _openedPos;
                })
                .OnComplete(() =>
                {
                    canvasGroup.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        private void OnEnable() => _canvasTransform.position = _closedPos;
    }
}

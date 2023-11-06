using System;
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base
{
    public abstract class CanvasGroupView : MonoBehaviour, IInitializable, ICanvasGroupView
    {
        [SerializeField] protected UnityEngine.CanvasGroup canvasGroup;

        [SerializeField] protected AnimationOptionsScriptable scriptable;
        
        public bool Interactable
        {
            get => canvasGroup.interactable;
            set => canvasGroup.interactable = value;
        }
        
        public abstract void Show(Action onComplete = null);
        
        public abstract void Hide(Action onComplete = null);
        public abstract void Init();
    }
}
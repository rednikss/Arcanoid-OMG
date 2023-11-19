using System;
using System.Threading.Tasks;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base
{
    public abstract class AnimatedCanvasGroupView : MonoInstaller, ICanvasGroupView
    {
        [SerializeField] protected UnityEngine.CanvasGroup canvasGroup;

        [SerializeField] protected AnimationOptionsScriptable scriptable;

        public abstract Task Show();
        
        public abstract Task Hide();

        public virtual void ImmediateEnable()
        {
            canvasGroup.interactable = true;
            gameObject.SetActive(true);
        }
        
        public virtual void ImmediateDisable()
        {
            canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }
    }
}
using System.Threading.Tasks;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.AnimatedViews.Game.HeartView
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        [SerializeField] private AnimationOptionsScriptable scriptable;
        
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        
        public async Task Disable()
        {
            if (DOTween.IsTweening(gameObject)) DOTween.Kill(gameObject);
            
            await image.DOColor(inactiveColor, scriptable.animationTime)
                .SetLink(gameObject)
                .SetEase(scriptable.showEase)
                .AsyncWaitForCompletion();
        }
        
        
        public async Task Enable()
        {
            if (DOTween.IsTweening(gameObject)) DOTween.Kill(gameObject);
            
            await image.DOColor(activeColor, scriptable.animationTime)
                .SetLink(gameObject)
                .SetEase(scriptable.hideEase)
                .AsyncWaitForCompletion();
        }

        public void ImmediateEnable() => image.color = activeColor;
        
        public void ImmediateDisable() => image.color = inactiveColor;
    }
}
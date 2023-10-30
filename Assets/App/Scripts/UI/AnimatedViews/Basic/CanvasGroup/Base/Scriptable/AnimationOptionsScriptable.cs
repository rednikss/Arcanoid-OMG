using DG.Tweening;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable
{
    [CreateAssetMenu(fileName = "Animation Options", menuName = "Scriptable Object/View/Animation Config", order = 0)]
    public class AnimationOptionsScriptable : ScriptableObject
    {
        [Min(0)] 
        public float animationTime = 0.25f;

        public Ease showEase = Ease.OutSine;

        public Ease hideEase = Ease.InSine;
    }
}
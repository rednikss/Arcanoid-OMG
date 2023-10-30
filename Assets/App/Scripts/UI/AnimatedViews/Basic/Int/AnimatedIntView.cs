using System.Text;
using App.Scripts.Architecture.MonoInitializable;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.UI.AnimatedViews.Basic.Int
{
    public class AnimatedIntView : MonoInitializable
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string prefix;
        [SerializeField] private string postfix;
        
        [SerializeField] [Min(0)] private float animationTime = 0.5f;

        private StringBuilder _builder;
        private int _value;

        public override void Init()
        {
            _builder = new(prefix);
            _builder.Append(0);
            _builder.Append(postfix);
        }
        
        public void SetValue(int value)
        {
            _builder.Replace(_value.ToString(), value.ToString());
            label.text = _builder.ToString();
            _value = value;
        }
        
        public void SetValueAnimated(int value)
        {
            DOTween.To(GetValue, SetValue, value, animationTime).SetUpdate(true).SetLink(gameObject);
        }

        
        public void SetValueAnimated(int value, float newAnimationTime)
        {
            DOTween.To(GetValue, SetValue, value, newAnimationTime).SetUpdate(true).SetLink(gameObject);
        }
        
        private int GetValue() => _value;
    }
}
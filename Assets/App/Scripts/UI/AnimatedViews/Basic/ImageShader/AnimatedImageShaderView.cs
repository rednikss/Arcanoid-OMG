using App.Scripts.Architecture.MonoInitializable;
using App.Scripts.UI.AnimatedViews.Basic.CanvasGroup.Base.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI.AnimatedViews.Basic.ImageShader
{
    public class AnimatedImageShaderView : MonoInitializable
    {
        [SerializeField] private RawImage image;
        
        [SerializeField] private Material shaderMaterial;

        [Header("Animation Options")]
        [SerializeField] private AnimationOptionsScriptable scriptable;
        
        [SerializeField] private string activeValueName;
        
        private float _activatedValue;
        
        private float _deactivatedValue;
        
        private Texture _sourceTexture;
        
        private Texture _shaderTexture;
        
        public override void Init()
        {
            shaderMaterial = new Material(shaderMaterial);
            _sourceTexture = image.texture;
            _shaderTexture = GetEffectTexture();
            
            _activatedValue = shaderMaterial.GetFloat(activeValueName);
            _deactivatedValue = 0;
        }

        private Texture GetEffectTexture()
        {
            RenderTexture current = RenderTexture.active;

            RenderTexture temp = RenderTexture.GetTemporary(_sourceTexture.width, _sourceTexture.height, 0);
            Graphics.Blit(_sourceTexture, temp, shaderMaterial);

            RenderTexture.active = temp;

            Texture2D blurTex = new Texture2D(temp.width, temp.height); 
            blurTex.ReadPixels(new Rect(0, 0, blurTex.width, blurTex.height), 0, 0, false);
            blurTex.Apply();
            
            RenderTexture.active = current;
            RenderTexture.ReleaseTemporary(temp);
            
            return blurTex;
        }

        public void ActivateShader()
        {
            image.texture = _shaderTexture;
        }
        
        public void DeactivateShader()
        {            
            image.texture = _sourceTexture;
        }
        
        public void ActivateShaderAnimated()
        {
            if (DOTween.IsTweening(gameObject)) DOTween.Kill(gameObject);

            DOTween.To(GetValue, SetValue, _activatedValue, scriptable.animationTime)
                .SetEase(scriptable.showEase)
                .SetLink(gameObject)
                .SetUpdate(true)
                .OnStart(() =>
                {
                    DeactivateShader();
                    SetValue(_deactivatedValue);
                    image.material = shaderMaterial;
                })
                .OnComplete(() =>
                {
                    ActivateShader();
                    image.material = null;
                });
        }
        
        
        public void DeactivateShaderAnimated()
        {
            if (DOTween.IsTweening(gameObject)) DOTween.Kill(gameObject);
            
            DOTween.To(GetValue, SetValue, _deactivatedValue, scriptable.animationTime)
                .SetEase(scriptable.showEase)
                .SetLink(gameObject)
                .SetUpdate(true)
                .OnStart(() =>
                {
                    DeactivateShader();
                    SetValue(_activatedValue);
                    image.material = shaderMaterial;
                })
                .OnComplete(() =>
                {
                    DeactivateShader();
                    SetValue(_activatedValue);
                    image.material = null;
                });
        }

        private void SetValue(float value) => shaderMaterial.SetFloat(activeValueName, value);

        private float GetValue() => shaderMaterial.GetFloat(activeValueName);
    }
}
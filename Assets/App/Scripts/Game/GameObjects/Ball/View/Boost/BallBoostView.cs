using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Ball.View.Boost
{
    public class BallBoostView : MonoInstaller
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color activeColor;
        [SerializeField] private ParticleSystem particle;

        private Color inactiveColor;
        
        public override void Init(ServiceContainer container)
        {
            inactiveColor = spriteRenderer.color;
        }

        public void SetBoostView(bool state)
        {
            spriteRenderer.color = state ? activeColor : inactiveColor;
            if (state) particle.Play();
            else particle.Stop();
        }
    }
}
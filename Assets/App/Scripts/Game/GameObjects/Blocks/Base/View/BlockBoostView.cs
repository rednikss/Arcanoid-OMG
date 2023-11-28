using App.Scripts.Game.GameObjects.Boost.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Blocks.Base.View
{
    public class BlockBoostView : MonoInstaller
    {
        [SerializeField] private BoostSpriteList scriptable;

        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public override void Init(ServiceContainer container)
        {
        }

        public void SetBoost(int id)
        {
            spriteRenderer.sprite = id < 0 ? null : scriptable.sprites[id];
        }
    }
}
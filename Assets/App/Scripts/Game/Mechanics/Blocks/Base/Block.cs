using System;
using App.Scripts.Game.Mechanics.Blocks.Base.Pool;
using App.Scripts.Game.Mechanics.Blocks.Base.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks.Base
{
    public class Block : MonoInstaller
    {
        [SerializeField] public BlockDataScriptable scriptable;

        public event Action<float> OnHealthChanged;

        private float health;
        private float Health
        {
            get => health;
            set
            {
                OnHealthChanged?.Invoke(value);
                health = value;
            }
        }

        private BlockPool pool;
        
        public override void Init(ServiceContainer container)
        {
            ResetHealth();
            pool = container.GetService<BlockPool>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            DecreaseHealth();
            
            if (!IsDead()) return;
            
            pool.ReturnObject(this, scriptable.blockID);
        }
        
        private void ResetHealth() => Health = scriptable.health;

        private void DecreaseHealth() => Health--;

        private bool IsDead() => Health == 0;
    }
}
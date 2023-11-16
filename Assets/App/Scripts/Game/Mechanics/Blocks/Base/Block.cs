using System;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks.Base
{
    public class Block : MonoInstaller
    {
        [SerializeField] public BlockDataScriptable scriptable;

        public event Action<Block> OnBlockDestroyed;
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

        public override void Init(ProjectContext context)
        {
            ResetHealth();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            DecreaseHealth();
            
            if (!IsDead()) return;
            
            OnBlockDestroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
        
        private void ResetHealth() => Health = scriptable.health;

        private void DecreaseHealth() => Health--;

        private bool IsDead() => Health == 0;
    }
}
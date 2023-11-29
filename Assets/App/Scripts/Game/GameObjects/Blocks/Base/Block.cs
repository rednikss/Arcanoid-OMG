using System;
using App.Scripts.Game.GameObjects.Blocks.Base.Pool;
using App.Scripts.Game.GameObjects.Blocks.Base.Scriptable;
using App.Scripts.Game.GameObjects.Blocks.Base.View;
using App.Scripts.Game.GameObjects.Boost.Base.Pool;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Blocks.Base
{
    public class Block : MonoInstaller
    {
        [SerializeField] private BlockDataScriptable scriptable;
        [SerializeField] private BlockHealthView healthView;
        [SerializeField] private BlockBoostView boostView;

        [SerializeField] private Collider2D _collider;

        public bool IsImmediateDestroy
        {
            get => _collider.isTrigger;
            set => _collider.isTrigger = value;
        }
        
        public int ID => scriptable.blockID;
        
        private int boostID = -1;

        private int health;
        private int Health
        {
            get => health;
            set
            {
                health = value;
                if (healthView != null) healthView.SetHealthPercent((float) health / scriptable.health);
            }
        }

        private BlockPool blockPool;
        private BoostPool boostPool;
        
        public override void Init(ServiceContainer container)
        {
            blockPool = container.GetService<BlockPool>(); 
            boostPool = container.GetService<BoostPool>();
            
            ResetHealth();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out Ball.Ball _)) return;
            
            RemoveBlock();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.collider.TryGetComponent(out Ball.Ball ball)) return;

            AddHealth(-ball.Damage);
            
            if (IsDead()) RemoveBlock();
        }

        public void SetBoost(int id)
        {
            boostID = id;
            if (boostView != null) boostView.SetBoost(id);
        }
        
        private void ResetHealth() => Health = scriptable.health;

        private void AddHealth(int amount)
        {
            if (Health < 0) return;
            Health = Math.Clamp(Health + amount, 0, scriptable.health);
        }

        private bool IsDead() => Health == 0;

        private void RemoveBlock()
        {
            blockPool.ReturnObject(this, scriptable.blockID);
            
            if (boostID < 0) return;
            
            var block = boostPool.Get(boostID);
            block.transform.position = transform.position;
        }
    }
}
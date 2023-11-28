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
        public int ID => scriptable.blockID;
        
        private int boostID = -1;

        private float health;
        private float Health
        {
            get => health;
            set
            {
                if (healthView != null) healthView.SetHealthPercent(health / scriptable.health);
                health = value;
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

        private void OnCollisionEnter2D(Collision2D col)
        {
            DecreaseHealth();
            
            if (!IsDead()) return;

            blockPool.ReturnObject(this, scriptable.blockID);
            
            if (boostID < 0) return;
            
            var block = boostPool.Get(boostID);
            block.transform.position = transform.position;
        }

        public void SetBoost(int id)
        {
            boostID = id;
            if (boostView != null) boostView.SetBoost(id);
        }
        
        private void ResetHealth() => Health = scriptable.health;

        private void DecreaseHealth() => Health--;

        private bool IsDead() => Health == 0;
    }
}
using System.Collections.Generic;
using App.Scripts.Libs.Patterns.Service.Container;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Blocks.Base.View
{
    public class BlockHealthView : BlockView
    {
        [SerializeField] private Block block;
        
        [SerializeField] private SpriteRenderer[] breaks;

        private readonly List<SpriteRenderer> healthBar = new();

        public override void Init(ServiceContainer container)
        {
            healthBar.Add(null);
            healthBar.AddRange(breaks);
            healthBar.Add(null);
        }
        
        
        public void SetHealthPercent(float percent)
        {
            int intCount = 1 + (int)((1 - percent) * healthBar.Count);

            for (int i = 0; i < healthBar.Count; i++)
            {
                if (healthBar[i] == null) continue;
                
                healthBar[i].enabled = i < intCount;
            }
        }
    }
}
using System.Collections.Generic;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks.Base
{
    public class BlockHealthView : BlockView
    {
        [SerializeField] private Block block;
        
        [SerializeField] private SpriteRenderer[] breaks;

        private readonly List<SpriteRenderer> healthBar = new();

        public override void Init(ProjectContext context)
        {
            block.OnHealthChanged += health => SetHealthPercent(health / block.scriptable.health);
            
            healthBar.Add(null);
            healthBar.AddRange(breaks);
            healthBar.Add(null);
        }
        
        private void SetHealthPercent(float percent)
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
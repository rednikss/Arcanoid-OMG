using App.Scripts.Game.Mechanics.Blocks.Base;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Game.Mechanics.Blocks
{
    public abstract class BlockView : MonoInstaller
    {
        [SerializeField] protected BlockTile tile;
    }
}
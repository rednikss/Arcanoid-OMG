using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Game.GameObjects.Blocks.Base.View
{
    public abstract class BlockView : MonoInstaller
    {
        [SerializeField] protected BlockTile tile;
    }
}
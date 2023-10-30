using UnityEngine;

namespace App.Scripts.Architecture.InitPoint.Additional
{
    public class AdditionalPoint : MonoInitializable.MonoInitializable
    {
        [SerializeField] private MonoInitializable.MonoInitializable[] monoInitializables;
        
        public override void Init()
        {
            foreach (var monoInitializable in  monoInitializables)
            {
                monoInitializable.Init();
            }
        }
    }
}
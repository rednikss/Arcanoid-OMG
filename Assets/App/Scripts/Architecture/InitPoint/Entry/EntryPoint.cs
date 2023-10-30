using UnityEngine;

namespace App.Scripts.Architecture.InitPoint.Entry
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MonoInitializable.MonoInitializable[] monoInitializables;
        
        public void Awake()
        {
            foreach (var monoInitializable in  monoInitializables)
            {
                monoInitializable.Init();
            }
        }
    }
}

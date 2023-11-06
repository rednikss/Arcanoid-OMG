using UnityEngine;

namespace App.Scripts.Architecture.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InitPoint.MonoInstaller.MonoInstaller[] monoInstallers;
        
        public void Awake()
        {
            var context = ProjectContext.ProjectContext.Instance;

            foreach (var monoInstaller in monoInstallers)
            {
                monoInstaller.Init(context);
            }
        }
    }
}

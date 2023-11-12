using UnityEngine;

namespace App.Scripts.Libs.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MonoInstaller.MonoInstaller[] monoInstallers;

        [SerializeReference] private MonoInstaller.MonoInstaller starter;
        
        public void Awake()
        {
            var context = ProjectContext.ProjectContext.Instance;

            foreach (var monoInstaller in monoInstallers)
            {
                monoInstaller.Init(context);
            }
            
            starter.Init(context);
        }
    }
}

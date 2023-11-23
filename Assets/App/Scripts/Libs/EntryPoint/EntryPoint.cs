using UnityEngine;

namespace App.Scripts.Libs.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MonoInstaller.MonoInstaller[] monoInstallers;
        
        public void Awake()
        {
            var container = ProjectContext.ProjectContext.Instance.GetContainer();

            foreach (var monoInstaller in monoInstallers)
            {
                monoInstaller.Init(container);
            }
        }
    }
}

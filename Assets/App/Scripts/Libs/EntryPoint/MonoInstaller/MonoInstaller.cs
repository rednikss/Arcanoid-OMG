using UnityEngine;

namespace App.Scripts.Libs.EntryPoint.MonoInstaller
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Init(Architecture.ProjectContext.ProjectContext context);
    }
}
using UnityEngine;

namespace App.Scripts.Architecture.InitPoint.MonoInstaller
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Init(ProjectContext.ProjectContext context);
    }
}
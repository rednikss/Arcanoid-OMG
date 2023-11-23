using App.Scripts.Architecture.Project.OptionsInstaller.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using App.Scripts.Libs.Patterns.Service.Container;
using App.Scripts.Libs.ProjectContext;
using UnityEngine;

namespace App.Scripts.Architecture.Project.OptionsInstaller
{
    public class OptionsInstaller : MonoInstaller
    {
        [SerializeField] private BaseOptionsScriptable scriptable;

        public override void Init(ServiceContainer container)
        {
            Application.targetFrameRate = scriptable.targetFPS;
        }
    }
}
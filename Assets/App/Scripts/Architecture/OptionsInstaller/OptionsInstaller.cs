using App.Scripts.Architecture.OptionsInstaller.Scriptable;
using App.Scripts.Libs.EntryPoint.MonoInstaller;
using UnityEngine;

namespace App.Scripts.Architecture.OptionsInstaller
{
    public class OptionsInstaller : MonoInstaller
    {
        [SerializeField] private BaseOptionsScriptable scriptable;

        public override void Init(ProjectContext.ProjectContext context)
        {
            Application.targetFrameRate = scriptable.targetFPS;
        }
    }
}
using App.Scripts.Architecture.InitPoint.MonoInstaller;
using App.Scripts.Architecture.OptionsInstaller.Scriptable;
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
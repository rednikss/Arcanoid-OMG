using App.Scripts.Architecture.OptionsInstaller.Scriptable;
using UnityEngine;

namespace App.Scripts.Architecture.OptionsInstaller
{
    public class OptionsInstaller : MonoInitializable.MonoInitializable
    {
        [SerializeField] private BaseOptionsScriptable scriptable;
        
        public override void Init()
        {
            Application.targetFrameRate = scriptable.targetFPS;
        }
    }
}
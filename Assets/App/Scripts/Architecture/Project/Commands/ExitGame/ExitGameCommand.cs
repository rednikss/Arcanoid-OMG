using App.Scripts.Libs.Patterns.Command;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Architecture.Project.Commands.ExitGame
{
    public class ExitGameCommand : ICommand<bool>
    {
        public bool Execute()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            return true;
        }
    }
}
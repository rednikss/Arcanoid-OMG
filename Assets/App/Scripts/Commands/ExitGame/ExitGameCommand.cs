using App.Scripts.Architecture.Patterns.Command;
using UnityEditor;

namespace App.Scripts.Commands.ExitGame
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
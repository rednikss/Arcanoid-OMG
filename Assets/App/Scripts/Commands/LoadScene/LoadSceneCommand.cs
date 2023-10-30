using App.Scripts.Architecture.Command;
using UnityEngine.SceneManagement;

namespace App.Scripts.Commands.LoadScene
{
    public class LoadSceneCommand : ICommand<bool>
    {
        private readonly string _sceneName;
        
        public LoadSceneCommand(string value)
        {
            _sceneName = value;
        }
        
        public bool Execute()
        {
            SceneManager.LoadScene(_sceneName);

            return true;
        }
    }
}
namespace App.Scripts.Libs.Patterns.Command
{
    public interface ICommand<out T>
    {
        public T Execute();
    }
}
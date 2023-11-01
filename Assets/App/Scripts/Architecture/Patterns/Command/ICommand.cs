namespace App.Scripts.Architecture.Patterns.Command
{
    public interface ICommand<out T>
    {
        public T Execute();
    }
}
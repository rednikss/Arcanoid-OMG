namespace App.Scripts.Architecture.Command
{
    public interface ICommand<out T>
    {
        public T Execute();
    }
}
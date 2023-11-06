namespace App.Scripts.Libs.Command
{
    public interface ICommand<out T>
    {
        public T Execute();
    }
}
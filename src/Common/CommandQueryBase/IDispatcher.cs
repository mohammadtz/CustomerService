namespace Common.CommandQueryBase;

public interface IDispatcher
{
    Task ExecuteCommandAsync(ICommand command);
}
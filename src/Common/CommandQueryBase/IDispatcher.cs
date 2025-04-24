namespace Common.CommandQueryBase;

public interface IDispatcher
{
    Task ExecuteCommandAsync(ICommand command);
    Task<TResponse> ExecuteQueryAsync<TResponse>(IQuery<TResponse> query);
}
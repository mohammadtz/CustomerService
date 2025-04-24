using MediatR;

namespace Common.CommandQueryBase;

public class Dispatcher(ISender sender) : IDispatcher
{
    public async Task ExecuteCommandAsync(ICommand command)
    {
        await sender.Send(command);
    }

    public async Task<TResponse> ExecuteQueryAsync<TResponse>(IQuery<TResponse> query)
    {
        return await sender.Send(query);
    }
}
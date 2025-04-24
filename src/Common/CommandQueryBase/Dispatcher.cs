using MediatR;

namespace Common.CommandQueryBase;

public class Dispatcher(ISender sender) : IDispatcher
{
    public async Task ExecuteCommandAsync(ICommand command)
    {
        await sender.Send(command);
    }
}
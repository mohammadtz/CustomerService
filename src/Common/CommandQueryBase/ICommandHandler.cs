using MediatR;

namespace Common.CommandQueryBase;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand;
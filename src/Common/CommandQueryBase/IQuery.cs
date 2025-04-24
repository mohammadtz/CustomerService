using MediatR;

namespace Common.CommandQueryBase;

public interface IQuery<out TResponse> : IRequest<TResponse>;
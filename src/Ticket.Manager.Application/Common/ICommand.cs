using MediatR;

namespace Ticket.Manager.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest;
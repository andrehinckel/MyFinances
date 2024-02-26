using MediatR;
using MyFinances.Common.Core.Results;

namespace MyFinances.Common.Core.Cqrs;

public interface ICommand : IRequest<Result>;

public interface ICommand<T> : IRequest<Result<T>> where T : notnull;
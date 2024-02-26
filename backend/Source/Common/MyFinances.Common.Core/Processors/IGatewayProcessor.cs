using MediatR;
using MyFinances.Common.Core.Modules;

namespace MyFinances.Common.Core.Processors;

public interface IGatewayProcessor<TModule>
    where TModule : class, IModuleConfiguration
{
    Task<T> ExecuteCommand<T>(Func<IMediator, Task<T>> action);
}
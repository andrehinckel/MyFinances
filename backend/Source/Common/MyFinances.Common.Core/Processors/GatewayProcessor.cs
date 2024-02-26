using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.Common.Core.Modules;
using MyFinances.Common.Core.Modules.RootConfiguration;

namespace MyFinances.Common.Core.Processors;

public class GatewayProcessor<TModule> : IGatewayProcessor<TModule>
    where TModule : class, IModuleConfiguration
{
    private readonly IServiceProvider _serviceProvider = 
        ModuleRootRegistry.GetByModule<TModule>().ServiceProvider;

    public async Task<T> ExecuteCommand<T>(Func<IMediator, Task<T>> action)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await action.Invoke(mediator);
    }
}
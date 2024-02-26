using Microsoft.Extensions.DependencyInjection;

namespace MyFinances.Common.Core.Modules.RootConfiguration;

public class CompositionRoot(
    IServiceCollection serviceCollection,
    IModuleConfiguration module) : ICompositionRoot
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IServiceCollection ServiceCollection { get; } = serviceCollection;
    public IModuleConfiguration ModuleDefinition { get; } = module;
    public void BuildServices() => ServiceProvider = ServiceCollection.BuildServiceProvider();
}
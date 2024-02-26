using Microsoft.Extensions.DependencyInjection;

namespace MyFinances.Common.Core.Modules.RootConfiguration;

public interface ICompositionRoot
{
    IServiceProvider ServiceProvider { get; }
    IServiceCollection ServiceCollection { get; }
    IModuleConfiguration ModuleDefinition { get; }
    void BuildServices();
}
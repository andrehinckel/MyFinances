using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MyFinances.Common.Core.Modules.RootConfiguration;
using MyFinances.Common.Core.Processors;

namespace MyFinances.Common.Core.Modules;

public static class ModuleExtensions
{
    public static void AddModulesServices(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] scanAssemblies)
    {
        var assemblies = scanAssemblies.Length != 0 ? scanAssemblies : AppDomain.CurrentDomain.GetAssemblies();

        var modules = assemblies
            .SelectMany(x => x.GetTypes()).Where(t =>
                t is { IsClass: true, IsAbstract: false } and { IsGenericType: false, IsInterface: false }
                && t.GetConstructor(Type.EmptyTypes) != null
                && typeof(IModuleConfiguration).IsAssignableFrom(t)).ToList();

        modules.ForEach(module =>
        {
            var newServiceCollection = new ServiceCollection();
            foreach (var service in services)
                newServiceCollection.Add(service);

            newServiceCollection.RemoveAll<IHostedService>();

            var instantiatedType = (IModuleConfiguration)Activator.CreateInstance(module)!;
            instantiatedType.AddModuleServices(newServiceCollection,
                configuration.GetSection(instantiatedType.ModuleName));

            ModuleRegistry.Add(instantiatedType);

            ModuleRootRegistry.Add(new CompositionRoot(newServiceCollection, instantiatedType));
        });

        services.Replace(ServiceDescriptor.Singleton(typeof(IGatewayProcessor<>), typeof(GatewayProcessor<>)));
    }

    public static void BuildModulesServices()
    {
        var modules = ModuleRegistry.ModuleConfigurations;

        foreach (var module in modules)
            ModuleRootRegistry.GetByModule(module).BuildServices();
    }

    public static void AddModuleConfiguration(
        this ConfigurationManager configurationManager,
        IConfiguration configuration,
        string environment,
        string root)
    {
        if (environment != Environments.Development &&
            environment != "Test")
        {
            configurationManager.AddAzureAppConfiguration(configuration.GetConnectionString("AppConfiguration"));
        }
        else
        {
            foreach (var file in Directory.GetFiles(root, "*.appsettings.json",
                         SearchOption.AllDirectories))
            {
                configurationManager.AddJsonFile(file);
            }
        }
    }

    public static async Task ConfigureModules(this WebApplication app)
    {
        var modules = ModuleRegistry.ModuleConfigurations;

        foreach (var module in modules)
        {
            var compositionRoot = ModuleRootRegistry.GetByModule(module);

            await module.ConfigureModuleAsync(
                new ApplicationBuilder(compositionRoot.ServiceProvider),
                app.Environment);
        }
    }

    public static void MapModulesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var modules = ModuleRegistry.ModuleConfigurations;

        foreach (var module in modules)
            module.MapModuleEndpoints(endpoints);
    }
}
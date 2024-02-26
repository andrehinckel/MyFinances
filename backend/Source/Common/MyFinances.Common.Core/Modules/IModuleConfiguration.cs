using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyFinances.Common.Core.Modules;

public interface IModuleConfiguration
{
    string ModuleName { get; }
    void AddModuleServices(IServiceCollection services, IConfiguration configuration);

    Task ConfigureModuleAsync(IApplicationBuilder app, IWebHostEnvironment environment);

    void MapModuleEndpoints(IEndpointRouteBuilder endpoints);
}
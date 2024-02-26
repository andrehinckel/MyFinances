using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFinances.Common.Core.BlobStorage;
using MyFinances.Common.Core.Email;
using MyFinances.Common.Core.Email.Brevo;
using MyFinances.Common.Core.RequestContext;
using MyFinances.Common.Core.UnitOfWork;

namespace MyFinances.Common.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(client =>
        {
            client.AddBlobServiceClient(configuration.GetConnectionString("BlobStorage"));
        });

        services.AddSingleton<IBlobStorageClient, BlobStorageClient>();
    }

    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestContext, RequestContext.RequestContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }

    public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BrevoOptions>(configuration.GetSection("Brevo"));
        services.AddSingleton<IEmailService, EmailService>();
    }

    public static void AddCustomDbContext<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>();
    }
}
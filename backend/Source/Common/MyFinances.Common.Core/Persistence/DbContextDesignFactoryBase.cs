// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;
//
// namespace MyFinances.Common.Core.Persistence;
//
// public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
//     where TDbContext : DbContext
// {
//     public TDbContext CreateDbContext(string[] args)
//     {
//         var builder = new ConfigurationBuilder()
//             .SetBasePath(AppContext.BaseDirectory)
//             .AddJsonFile("appsettings.json")
//             .AddEnvironmentVariables()
//             .Build();
//
//         var connectionString = builder.GetConnectionString("MySQL")!;
//
//         var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
//             
//             .UseSnakeCaseNamingConvention();
//
//         return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options)!;
//     }
// }
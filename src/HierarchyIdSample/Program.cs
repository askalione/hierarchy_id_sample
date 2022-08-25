using HierarchyIdSample;
using HierarchyIdSample.ApplicationServices;
using HierarchyIdSample.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

const string connectionString =
    @"Host=localhost;Port=5432;Database=hierarchy_id;Username=some_user;Password=some_password";

var services = new ServiceCollection();

services.AddOperations(Assembly.GetExecutingAssembly());
services.AddDbContext<IDbContext, AppDbContext>(dbContextOptions =>
{
    dbContextOptions
        .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention();
});
services.AddSingleton<IDbConnectionFactory>(new AppDbConnectionFactory(connectionString));
services.AddTransient<ICounterService, CounterService>();
services.AddTransient<App>();

IServiceProvider serviceProvider = services.BuildServiceProvider();

AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.EnsureCreated();
if (dbContext.Counters.Any() == false)
{
    dbContext.Counters.Add(new Counter("DepartmentId", 0));
    dbContext.SaveChanges();
}

await serviceProvider.GetRequiredService<App>()
    .RunAsync();
using coe.dnd.dal.Contexts;
using coe.dnd.dal.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace coe.dnd.integration.test.Base;

public class IntegrationClassFixture : IDisposable
{
    public readonly WebApplicationFactory<Program> Host;

    public IntegrationClassFixture()
    {
        Host = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(e =>
                {
                    e.AddDbContext<DndOrganiserContext>(options => options
                            .EnableSensitiveDataLogging()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString()),
                        ServiceLifetime.Singleton,
                        ServiceLifetime.Singleton);
                    e.AddTransient<IDndOrganiserDatabase, DndOrganiserContext>();
                });
            });
        
        DatabaseSeed.SeedDatabase(Host.Services.GetService<DndOrganiserContext>());
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}
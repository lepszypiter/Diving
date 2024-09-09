using Diving.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Diving.Infrastructure;

public static class DivingInfrastructure
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
       services.AddDbContext<DivingContext>(opt =>
           opt.UseSqlite("Data Source=C:\\DB\\diving.db"));

        services.AddScoped<IClientRepository, ClientRepository>();
    }
}

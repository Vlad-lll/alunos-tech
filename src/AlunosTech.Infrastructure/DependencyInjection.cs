using AlunosTech.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AlunosTech.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IFaturaRepository, FaturaRepository>();
        return services;
    }
}

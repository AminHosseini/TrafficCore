using Infrastructure.Implementations.CensusAggregate.Isar;

namespace Api.Bootstrapper.CensusAggregate;

public static class IsarBootstrapper
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IMoserinRepository, MoserinRepository>();
    }
}

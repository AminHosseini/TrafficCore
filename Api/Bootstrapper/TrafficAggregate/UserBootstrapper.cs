namespace Api.Bootstrapper.TrafficAggregate;
public static class UserBootstrapper
{
    public static void Configure(IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
    }
}

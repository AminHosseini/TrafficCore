namespace Domain.Interfaces.TrafficAggregate.User;
public interface IUserRepository
{
    Task<IEnumerable<GetUsersModel>> GetUsersAsync();
}

namespace Infrastructure.Implementations.TrafficAggregate.User;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetUsersModel>> GetUsersAsync()
    {
        var query = "SELECT * FROM [Users]";
        using (var connection = _context.CreateConnection())
        {
            var users = await connection.QueryAsync<GetUsersModel>(query);
            return users.ToList();
        }
    }
}

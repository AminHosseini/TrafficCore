namespace Api.Controllers.TrafficAggregate.User;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<GetUsersModel>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }
}

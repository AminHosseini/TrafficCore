namespace Domain.Entities.TrafficAggregate.Account;
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    [SetsRequiredMembers]
    public LoginModel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
}

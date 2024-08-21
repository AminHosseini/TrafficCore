namespace Domain.Entities.TrafficAggregate.User;
public class GetUsersModel
{
    public long Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    [SetsRequiredMembers]
    public GetUsersModel()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
        Email = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
    }
}

namespace DataAccess;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string HashPassword { get; set; }
}
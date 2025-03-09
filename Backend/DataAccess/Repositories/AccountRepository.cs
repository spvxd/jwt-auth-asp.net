namespace DataAccess.Repositories;

public class AccountRepository
{
    private static IDictionary<string, User> Users = new Dictionary<string, User>();
    public void Add(User user)
    {
        Users[user.Username] = user;
    }

    public User FindByUsername(string username)
    {
        return Users.TryGetValue(username, out var user) ? user : null;
    }
}
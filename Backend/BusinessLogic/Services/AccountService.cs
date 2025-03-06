using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services;

public class AccountService(AccountRepository repository)
{
    public void Register(string username, string firstName, string password)
    {
        var account = new User
        {
            Username = username,
            FirstName = firstName,
            Id = Guid.NewGuid()
        };
        var passwordHash = new PasswordHasher<User>().HashPassword(account, password);
        account.HashPassword = passwordHash;
        repository.Add(account);
    }

    public void Login(string username, string password)
    {
        var account = repository.FindByUsername(username);
        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.HashPassword, password);
        if (result == PasswordVerificationResult.Success)
        {
            throw new Exception("All good");
        }
        else
        {
            throw new Exception($"Wrong password: {password}");
        }
        
    }
}
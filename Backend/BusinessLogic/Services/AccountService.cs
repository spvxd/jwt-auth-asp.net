using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services;

public class AccountService(AccountRepository repository, JwtService jwtService)
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

    public string Login(string username, string password)
    {
        var account = repository.FindByUsername(username);
        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.HashPassword, password);
        if (result == PasswordVerificationResult.Success)
        {
           return jwtService.GenerateToken(account);
        }
        else
        {
            throw new Exception($"Wrong password: {password}");
        }
        
    }
}
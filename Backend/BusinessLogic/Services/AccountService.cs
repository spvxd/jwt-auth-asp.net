using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services;

public class AccountService
{
    private readonly AccountRepository _repository;
    private readonly JwtService _jwtService;

    public AccountService(AccountRepository repository, JwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async void Register(string username, string firstName, string password)
    {
        var account = new User
        {
            Username = username,
            FirstName = firstName,
            Id = Guid.NewGuid()
        };
        var passwordHash = new PasswordHasher<User>().HashPassword(account, password);
        account.HashPassword = passwordHash;
        await _repository.AddNewUserAsync(account);
    }

    public async Task<string> Login(string username, string password)
    {
        var account = await _repository.FindByUsername(username);
        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.HashPassword, password);
        if (result == PasswordVerificationResult.Success)
        {
           return _jwtService.GenerateToken(account);
        }
        else
        {
            throw new Exception($"Wrong password: {password}");
        }
        
    }
}
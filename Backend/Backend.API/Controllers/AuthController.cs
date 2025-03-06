using Backend.API.DTO;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(AccountService accountService) : ControllerBase
{

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto registerRequest)
    {
        accountService.Register(registerRequest.Username, registerRequest.FirstName, registerRequest.Password);
        return Ok();
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginRequest)
    {
        accountService.Login(loginRequest.Username, loginRequest.Password);
        return Ok();
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}
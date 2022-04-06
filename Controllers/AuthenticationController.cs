using Microsoft.AspNetCore.Mvc;
using Base.Services;
using Base.Models.Requests;

namespace Base.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    readonly AuthenticationService _authenticationService;

    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegistrationRequest request)
    {
        var newUser = _authenticationService.Register(request.Username, request.Password);

        if (newUser is not null)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Invalid username or password");
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var (success, content) = _authenticationService.Login(request.Username, request.Password);
        if (success)
        {
            return Ok(content);
        }
        else
        {
            return BadRequest(content);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Base.Services;
using Base.Models.Requests;

namespace Base.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegistrationRequest request)
    {
        var (success, error, user) = _authenticationService.Register(request.Username, request.Password);

        if (success)
        {
            return CreatedAtAction(
                nameof(UserController.Get),
                "User",
                new { id = user!.Id },
                user
            );
        }
        else
        {
            return BadRequest(error);
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

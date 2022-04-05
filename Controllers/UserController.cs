using Microsoft.AspNetCore.Mvc;
using Base.Services;
using Base.Models;
using Base.Models.Requests;

namespace Base.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(long id)
    {
        var foundUser = _userService.GetById(id);

        if (foundUser is null)
        {
            return NotFound();
        }
        else
        {
            return foundUser;
        }
    }

    [HttpPost("register")]
    public IActionResult Register(RegistrationRequest request)
    {
        var newUser = _userService.Register(request.Username, request.Password);

        if (newUser is not null)
        {
            return CreatedAtAction(nameof(Get), new { id = newUser!.Id }, newUser);
        }
        else
        {
            return BadRequest("Invalid username or password");
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var (success, content) = _userService.Login(request.Username, request.Password);
        if (success)
        {
            return Ok(content);
        }
        else
        {
            return BadRequest(content);
        }
    }

    // [HttpPut("{id}")]
    // public IActionResult Update(long id, User updatedUser)
    // {

    // }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        var userToDelete = _userService.GetById(id);

        if (userToDelete is not null)
        {
            _userService.DeleteById(userToDelete.Id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}

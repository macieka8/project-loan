using Microsoft.AspNetCore.Mvc;
using Base.Services;
using Base.Models;
using Microsoft.AspNetCore.Authorization;

namespace Base.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get([FromRoute] long id)
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

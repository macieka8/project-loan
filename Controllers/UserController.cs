using Microsoft.AspNetCore.Mvc;
using Base.Services;
using Base.Models;

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

    [HttpPost]
    public IActionResult Create(User newUser)
    {
        var user = _userService.Create(newUser);
        return CreatedAtAction(nameof(Get), new { id = user!.Id }, user);
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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace SimpleNotes.Api.Controllers;

/*
Here should be the server response
*/

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // DI
    private readonly UserController _userController;

    public UserController(UserController userController)
    {
        _userController = userController;
    }

    [HttpGet] // Get all user


    [HttpGet("{id}")] // Get by Id


    [HttpPost] // Create user


    [HttpPut("{id}")] // Update user
    public IActionResult UpdateUser()
    {
        return Ok();
    }

    [HttpDelete("{id}")] // Delete user
    public IActionResult DeleteUser()
    {
        return Ok();
    }
}

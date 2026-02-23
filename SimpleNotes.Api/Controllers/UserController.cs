using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain.Mapping;

namespace SimpleNotes.Api.Controllers;

/*
Here should be the server response
*/

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // DI
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult GetAllUsers()
    {
        // Convert to Response
        var users = _userService.ListUsers().Select(user => user.ToResponse());
        // return all users
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult GetUserId(int id)
    {
        // Find Id
        var user = _userService.GetUser(id);
        // Error handling
        if (user == null) return NotFound();
        // Convert to Response
        var userResponse = user.ToResponse();

        // Return id user
        return Ok(userResponse);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest user)
    {
        // Convert to Domain
        var userDomain = user.ToDomain();
        // Call the service
        var userCreated = _userService.CreateUser(userDomain);
        // Convert to Response
        var userResponse = userCreated.ToResponse();
        // Return ok + user
        return CreatedAtAction(nameof(GetUserId), new { id = userResponse.Id }, userResponse);
    }

    [HttpPut("{id}")]
    public ActionResult<UserResponse> UpdateUser(int id, [FromBody] UpdateUserRequest user)
    {
        // Convert to Domain
        var userDomain = user.ToDomain(id);
        // Call the service
        var userUpdated = _userService.UpdateUser(userDomain);
        // Error handling
        if (!userUpdated) return NotFound();
        // Create Response
        var userResponse = userDomain.ToResponse();

        return Ok(userResponse);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // Error handling
        var deleted = _userService.DeleteUser(id);

        if (!deleted) return NotFound();

        return Ok();
    }
}

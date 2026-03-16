using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Api.Common;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;

namespace SimpleNotes.Api.Controllers;

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
    public IActionResult GetAllUsers()
    {
        // Convert to Response
        var users = _userService.ListUsers().Map(user => user.Select(u => u.ToResponse()));

        if (users.IsFailed)
        {
            return users.GetFailedActionResult();
        }
        // return all users
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetUserId(int id)
    {
        var user = _userService.GetUser(id);

        // If there IS a note
        if (user.IsSuccess && user.Value != null)
        {
            var responseNote = user.Value.ToResponse();
            return Ok(responseNote);
        }

        // If there ISNT a note
        return user.GetFailedActionResult();
    }

    [HttpGet("{email:string}")]
    public IActionResult GetByEmail(string email)
    {
        if (!TryCreateEmailText(email, out EmailText emailText))
        {
            //return BadRequest
        }

        var userEmail = _userService.GetByEmail(emailText);

        if (userEmail == null) return NotFound();

        var userResponse = userEmail.ToResponse();

        return Ok(userResponse);
    }

    private bool TryCreateEmailText(string emailString, out EmailText email)
    {
        try
        {
            email = EmailText.Create(emailString);
            return true;
        }
        catch (Exception)
        {
            email = default!;
            return false;
        }
    }
    // Get all the notes with the user Id
    /*[HttpGet("{id}/note")]
    public IActionResult GetUserNotes(int id)
    {
        // USING UserId, Find Notes
        var user = _userService.GetUser(id);
        // Error handling
        if (user.IsFailed)
        {
            return user.GetFailedActionResult();
        }
        // List all notes related to user
        var listNotes = _userService.GetUserNotes(user.Id); // List of notes related to User
        // Convert to response
        var userResponse = listNotes.Select(d => d.ToResponse());

        // return Ok(notesFound)
        return Ok(userResponse);
    }*/

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserRequest user)
    {
        // Convert to Domain
        var userDomain = user.ToDomain();
        // Call the service
        var userCreated = _userService.CreateUser(userDomain);
        // Convert to Response
        if (userCreated.IsFailed)
        {

        }

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

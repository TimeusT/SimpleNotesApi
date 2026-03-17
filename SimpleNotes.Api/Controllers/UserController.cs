using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Api.Common;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;
using SimpleNotes.Infrastructure;

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

    [HttpGet("{email}")]
    public IActionResult GetByEmail(string email)
    {
        if (!TryCreateEmailText(email, out EmailText emailText))
        {
            //return BadRequest
            var validationError = new ValidationError().WithError("Email", "Invalid email format.");
            return BadRequest(validationError);
        }

        var userEmail = _userService.GetByEmail(emailText);

        if (userEmail.IsSuccess && userEmail.Value != null)
        {
            var userResponse = userEmail.Value.ToResponse();
            return Ok(userResponse);
        }

        return userEmail.GetFailedActionResult();
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
    [HttpGet("{id}/note")]
    public IActionResult GetUserNotes(int id)
    {
        // USING UserId, Find Notes
        var user = _userService.GetUser(id);
        // Error handling
        if (user.IsFailed)
        {
            return user.GetFailedActionResult();
        }
        // List all notes with same userID
        var listNotes = _userService.GetUserNotes(id);

        // To response
        var noteResponse = listNotes.Value.Select(n => n.ToResponse());
        // return Ok(notesFound)
        return Ok(noteResponse);
    }

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
            return userCreated.GetFailedActionResult();
        }

        var userResponse = userCreated.Value.ToResponse();
        // Return ok + user
        return CreatedAtAction(nameof(GetUserId), new { id = userResponse.Id }, userResponse);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserRequest user)
    {
        // Convert to Domain
        var userDomain = user.ToDomain(id);
        // Call the service
        var userUpdated = _userService.UpdateUser(userDomain);
        // Error handling
        if (userUpdated.IsFailed)
        {
            return userUpdated.GetFailedActionResult();
        }
        // Create Response
        var userResponse = userDomain.ToResponse();

        return Ok(userResponse);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        // Error handling
        var deleted = _userService.DeleteUser(id);

        if (deleted.IsFailed)
        {
            return deleted.GetFailedActionResult();
        }

        return NoContent();
    }
}

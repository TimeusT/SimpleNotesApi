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
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        // Convert to Response
        var users = await _userService.ListUsersAsync(cancellationToken);

        if (users.IsFailed)
        {
            return users.GetFailedActionResult();
        }

        // return all users
        var responseUsers = users.Value.Select(x => x.ToResponse());

        return Ok(responseUsers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserId(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserAsync(id, cancellationToken);

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
    public async Task<IActionResult> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (!TryCreateEmailText(email, out EmailText emailText))
        {
            //return BadRequest
            var validationError = new ValidationError().WithError("Email", "Invalid email format.");
            return BadRequest(validationError);
        }

        var userEmail = await _userService.GetByEmailAsync(emailText, cancellationToken);

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
    public async Task<IActionResult> GetUserNotes(int id, CancellationToken cancellationToken)
    {
        // USING UserId, Find Notes
        var user = await _userService.GetUserAsync(id, cancellationToken);

        if (user.IsFailed)
        {
            return user.GetFailedActionResult();
        }

        // List all notes with same userID
        var listNotes = await _userService.GetUserNotesAsync(id, cancellationToken);

        // To response
        var noteResponse = listNotes.Value.Select(n => n.ToResponse());

        // return Ok(notesFound)
        return Ok(noteResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest user, CancellationToken cancellationToken)
    {
        var userDomain = user.ToDomain();

        // Call the service
        var userCreated = await _userService.CreateUserAsync(userDomain, cancellationToken);

        if (userCreated.IsFailed)
        {
            return userCreated.GetFailedActionResult();
        }

        // Convert to response
        var userResponse = userCreated.Value.ToResponse();
        
        // Return ok + user
        return CreatedAtAction(nameof(GetUserId), new { id = userResponse.Id }, userResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserRequest user, CancellationToken cancellationToken)
    {
        var userDomain = user.ToDomain(id);

        // Call the service
        var userUpdated = await _userService.UpdateUserAsync(userDomain, cancellationToken);

        if (userUpdated.IsFailed)
        {
            return userUpdated.GetFailedActionResult();
        }

        // Create Response
        var userResponse = userDomain.ToResponse();

        return Ok(userResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var deleted = await _userService.DeleteUserAsync(id, cancellationToken);

        if (deleted.IsFailed)
        {
            return deleted.GetFailedActionResult();
        }

        return NoContent();
    }
}

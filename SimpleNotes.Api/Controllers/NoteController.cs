using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Api.Common;
using SimpleNotes.Application.DTOs;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;

namespace SimpleNotes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var notes = await _noteService.ListAsync(cancellationToken);

        if (notes.IsFailed)
        {
            return notes.GetFailedActionResult();
        }

        var noteResponse = notes.Value.Select(x => x.ToResponse());

        return Ok(notes.Value);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetAsync))]
    public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
    {
        var existingNote = await _noteService.GetAsync(id, cancellationToken);

        if (existingNote.IsSuccess && existingNote.Value != null)
        {
            var responseNote = existingNote.Value.ToResponse();
            return Ok(responseNote);
        }

        return existingNote.GetFailedActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateNoteRequest noteItem, CancellationToken cancellationToken)
    {
        var domainNote = noteItem.ToDomain();

        var createNoteResult = await _noteService.CreateAsync(domainNote, cancellationToken);

        if (createNoteResult.IsFailed)
        {
            return createNoteResult.GetFailedActionResult();
        }

        var responseNote = createNoteResult.Value.ToResponse();

        return CreatedAtAction(nameof(GetAsync), new { id = responseNote.Id }, responseNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateNoteRequest noteItem, CancellationToken cancellationToken)
    {
        var domainNote = noteItem.ToDomain(id);

        var updatedNote = await _noteService.UpdateAsync(domainNote, cancellationToken);

        if (updatedNote.IsFailed)
        {
            return updatedNote.GetFailedActionResult();
        }

        var responseNote = domainNote.ToResponse();

        return Ok(responseNote);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var deleted = await _noteService.DeleteAsync(id, cancellationToken);

        if (deleted.IsFailed)
        {
            return deleted.GetFailedActionResult();
        }

        return NoContent();
    }
}

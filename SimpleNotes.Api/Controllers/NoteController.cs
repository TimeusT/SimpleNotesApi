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
    // This is where you use DI to get an instance of the NoteService class
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var notes = _noteService.List();

        if (notes.IsFailed)
        {
            return notes.GetFailedActionResult();
        }

        var noteResponse = notes.Value.Select(x => x.ToResponse());
        return Ok(notes.Value);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string uniqueId)
    {
        var existingNote = _noteService.Get(uniqueId);

        // If there IS a note
        if (existingNote.IsSuccess && existingNote.Value != null)
        {
            var responseNote = existingNote.Value.ToResponse();
            return Ok(responseNote);
        }

        // If there ISNT a note
        return existingNote.GetFailedActionResult();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateNoteRequest noteItem)
    {
        // turn to domain
        var domainNote = noteItem.ToDomain();

        // call the service
        var createNoteResult = _noteService.Create(domainNote);

        if (createNoteResult.IsFailed)
        {
            return createNoteResult.GetFailedActionResult();
        }

        var responseNote = createNoteResult.Value.ToResponse();

        return CreatedAtAction(nameof(GetById), new { id = responseNote.Id }, responseNote);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string uniqueId, [FromBody] UpdateNoteRequest noteItem)
    {
        // Request to domain
        var domainNote = noteItem.ToDomain(uniqueId);

        // call the service
        var updatedNote = _noteService.Update(domainNote);

        // check update response
        if (updatedNote.IsFailed)
        {
            return updatedNote.GetFailedActionResult();
        }

        // create response
        var responseNote = domainNote.ToResponse();

        return Ok(responseNote);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string uniqueId)
    {
        var deleted = _noteService.Delete(uniqueId);

        if (deleted.IsFailed)
        {
            return deleted.GetFailedActionResult();
        }

        return NoContent();
    }
}

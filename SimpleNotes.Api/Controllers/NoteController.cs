using Microsoft.AspNetCore.Mvc;
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

    // This is where you use DI to get an instance of the NoteService class
    [HttpGet]
    public ActionResult<IEnumerable<NoteResponse>> GetAll()
    {
        //TODO call service and map to response
        var notes = _noteService.List().Select(note => note.ToResponse());
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public ActionResult<NoteResponse> GetById(int id)
    {
        var existingNote = _noteService.Get(id);

        if (existingNote == null) return NotFound();

        //TODO Map domain to response
        var responseNote = existingNote.ToResponse();

        return Ok(responseNote);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateNoteRequest noteItem)
    {
        //TODO map request to domain and call service
        // turn to domain
        var domainNote = noteItem.ToDomain();
        // call the service
        var createNote = _noteService.Create(domainNote);

        var responseNote = createNote.ToResponse();

        return CreatedAtAction(nameof(GetById), new { id = responseNote.Id }, responseNote);
    }

    [HttpPut("{id}")]
    public ActionResult<NoteResponse> Update([FromBody] UpdateNoteRequest noteItem)
    {
        //TODO map request to domain and call service
        // Request to domain
        var domainNote = noteItem.ToDomain();

        // call the service
        var updatedNote = _noteService.Update(domainNote);

        // check update response
        if (!updatedNote)
            return NotFound();

        // create response
        var responseNote = domainNote.ToResponse();

        return Ok(responseNote);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _noteService.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

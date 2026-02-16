using Microsoft.AspNetCore.Mvc;
using SimpleNotesApi.Models;
using SimpleNotesApi.Services;

namespace SimpleNotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        // Constructor Injection
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // This is where you use DI to get an instance of the NoteService class
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_noteService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var existingNote = _noteService.GetById(id);

            if (existingNote == null) return NotFound();

            return Ok(existingNote);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOrUpdateNoteRequest noteItem)
        {
            var createdNote = _noteService.CreateNote(noteItem);

            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id },createdNote);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] CreateOrUpdateNoteRequest noteItem)
        {
            var updated = _noteService.Update(id, noteItem);

            if (!updated)
                return NotFound();

            return Ok(updated);
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
}

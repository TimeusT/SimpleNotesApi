using Microsoft.AspNetCore.Mvc;
using SimpleNotesApi.Services;
using SimpleNotesApi.Services.Domain;

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
            var notes = _noteService.List().Select(note => note.ToResponse());

            return Ok(existingNote);
        }

        [HttpPost]
        public ActionResult<NoteResponse> Create([FromBody] CreateNoteRequest noteItem)
        {
            //TODO map request to domain and call service
            var notes = _noteService.List().Select(note => note.ToResponse());

            var createdNote = _noteService.Create(notes);

            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }

        [HttpPut("{id}")]
        public ActionResult<NoteResponse> Update([FromBody] UpdateNoteRequest noteItem)
        {
            //TODO map request to domain and call service
            var notes = _noteService.List().Select(note => note.ToResponse());

            var updated = _noteService.Update(notes);

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

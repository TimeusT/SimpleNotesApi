using Microsoft.AspNetCore.Http;
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
        // GetById
        // IF note doesnt exist, return NotFound()
        public IActionResult GetById(int id)
        {
            if (_noteService.GetById(id) == null)
            {
                return NotFound();
            }
            
            return Ok(_noteService.GetById(id));
        }

        [HttpPost]
        // CreateNote
        // RETURN new ID of the created note
        public IActionResult Create(NoteItem noteItem)
        {
            return Ok(_noteService.CreateNote(noteItem));
        }

        [HttpPut]
        // Update
        // IF note doesnt exist, return NotFound()
        public IActionResult Update(int id, NoteItem noteItem)
        {
            if (_noteService.GetById(id) == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        // Delete
        // IF note doesnt exist, return NotFound()
        public IActionResult Delete(int id)
        {
            if (_noteService.GetById(id) == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
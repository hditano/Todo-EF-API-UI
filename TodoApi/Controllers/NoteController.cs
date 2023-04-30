using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TodoApi.Models;
using TodoApi.Services.NoteService;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteservice;

        public NoteController(INoteService noteservice)
        {
            _noteservice = noteservice;
        }

        // We get all the Notes
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetAllNotes()
        {
            var result = await _noteservice.GetAllNotes();
            return Ok(result);
        }


        // We get all the specific note that matches the id
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetSingleNote(int id)
        {
            var note = await _noteservice.GetSingleNote(id);
            if (note is null)
                return NotFound("Note does not exist");
            return Ok(note);
        }
        
        // Post a new note, Id will be based on notes List count + 1, to make correlative
        [HttpPost]
        public async Task<ActionResult<Note>> AddNote(Note note)
        {
            var result = await _noteservice.AddNote(note);
            return Ok(result);
        }

        // Will update a note. First it will search if the id provider after the /api/Note/X is on the Notes.
        // If so, it will update the Note Json provide as request, to update all the values ( Title and Text)
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Note>>> UpdateNote(int id, Note request)
        {
            var note = await _noteservice.UpdateNote(id, request);
            if (note is null)
                return NotFound();

            return Ok(note);
            
        }
        
        // Will delete a note. First it will search for the 

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Note>>> DeleteNote(int id)
        {
            var note = await _noteservice.DeleteNote(id);
            if (note is null)
                return NotFound("Note was not found");

            return Ok($"Note with id {id} deleted");

        }

    }
}

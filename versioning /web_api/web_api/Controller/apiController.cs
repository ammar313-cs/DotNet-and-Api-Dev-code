using Microsoft.AspNetCore.Mvc;
using WebApi.Models; // Make sure to include this namespace
using WebApi.DbHandler;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")] // Declare the API version
    [Route("api/v{version:apiVersion}/Note")]
    public class NoteController : ControllerBase
    {
        private readonly JsonFileDb _jsonFileDb;
        

        public NoteController(JsonFileDb jsonFileDb)
        {
            _jsonFileDb = jsonFileDb;
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            List<Note> notes = await _jsonFileDb.GetAllNotesAsync();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> AddNote(Note note)
        {
            List<Note> notes = await _jsonFileDb.GetAllNotesAsync();
            int newId = notes.Max(n => n.Id) + 1;
            note.Id = newId;
            notes.Add(note);

            await _jsonFileDb.SaveNotesAsync(notes);

            return CreatedAtAction(nameof(GetNotes), notes);
        }
    }
}

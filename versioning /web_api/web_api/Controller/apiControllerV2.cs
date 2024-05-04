using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.DbHandler;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Note")]
    public class NoteControllerV2 : ControllerBase
    {
        private readonly JsonFileDb _jsonFileDb;

        public NoteControllerV2(JsonFileDb jsonFileDb)
        {
            _jsonFileDb = jsonFileDb;
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            Note? note = await _jsonFileDb.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNoteById(int id, Note updatedNote)
        {
            List<Note> notes = await _jsonFileDb.GetAllNotesAsync();
            Note? existingNote = notes.FirstOrDefault(n => n.Id == id);
            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;

            await _jsonFileDb.SaveNotesAsync(notes);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNoteById(int id)
        {
            List<Note> notes = await _jsonFileDb.GetAllNotesAsync();
            Note? noteToDelete = notes.FirstOrDefault(n => n.Id == id);
            if (noteToDelete == null)
            {
                return NotFound();
            }

            notes.Remove(noteToDelete);

            await _jsonFileDb.SaveNotesAsync(notes);

            return NoContent();
        }
    }
}

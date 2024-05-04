using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DbHandler
{
    public class JsonFileDb
    {
       private readonly string _dbFilePath = "/Volumes/OVERLORD1 3/dotnet_files/versioning /web_api/web_api/Data/notes.json";

        public async Task<List<Note>> GetAllNotesAsync()
        {
            try
            {
                using (FileStream fs = File.OpenRead(_dbFilePath))
                {
                    return await JsonSerializer.DeserializeAsync<List<Note>>(fs) ?? new List<Note>();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here (logging, error handling, etc.)
                Console.WriteLine($"An error occurred while deserializing JSON: {ex}");
                return new List<Note>();
            }
        }

        public async Task SaveNotesAsync(List<Note> notes)
        {
            try
            {
                using (FileStream fs = File.Create(_dbFilePath))
                {
                    await JsonSerializer.SerializeAsync(fs, notes);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here (logging, error handling, etc.)
                Console.WriteLine($"An error occurred while serializing JSON: {ex}");
            }
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var notes = await GetAllNotesAsync();
            var noteToRemove = notes.FirstOrDefault(n => n.Id == id);

            if (noteToRemove == null)
            {
                return false;
            }

            notes.Remove(noteToRemove);
            await SaveNotesAsync(notes);

            return true;
        }

        public async Task<bool> UpdateNoteAsync(Note updatedNote)
        {
            var notes = await GetAllNotesAsync();
            var existingNote = notes.FirstOrDefault(n => n.Id == updatedNote.Id);

            if (existingNote == null)
            {
                return false;
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;

            await SaveNotesAsync(notes);

            return true;
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            var notes = await GetAllNotesAsync();
            return notes.FirstOrDefault(n => n.Id == id);
        }
    }
}

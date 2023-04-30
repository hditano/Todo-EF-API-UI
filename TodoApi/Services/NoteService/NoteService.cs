using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

namespace TodoApi.Services.NoteService
{
    public class NoteService : INoteService
    {

        private readonly DataContext _context;

        public NoteService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> AddNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note is null)
                return null;

            _context.Remove(note);
            await _context.SaveChangesAsync();
            return await _context.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllNotes()
        {
            var note = await _context.Notes.ToListAsync();
            return note;
        }

        public async Task<Note> GetSingleNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note is null)
                return null;
            return note;
        }

        public async Task<List<Note>> UpdateNote(int id, Note request)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note is null)
                return null;

            note.Title = request.Title;
            note.Text = request.Text;

            await _context.SaveChangesAsync();

            return await _context.Notes.ToListAsync();
        }
    }
}

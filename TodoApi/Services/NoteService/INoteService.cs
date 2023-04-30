using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Services.NoteService
{
    public interface INoteService
    {
        Task <List<Note>> GetAllNotes();
        Task <Note> GetSingleNote(int id);
        Task <List<Note>> AddNote(Note note);
        Task <List<Note>> UpdateNote(int id, Note note);
        Task <List<Note>> DeleteNote(int id);

    }
}

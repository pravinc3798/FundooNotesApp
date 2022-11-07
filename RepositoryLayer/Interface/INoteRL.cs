using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNotes(NoteModel noteModel, long userId);
        public IEnumerable<NoteEntity> ViewNotes(long userId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity EditNote(NoteModel noteModel, long userId, long noteId);
        public bool ArchiveNote(long userId, long noteId);
        public bool TrashNote(long userId, long noteId);
        public bool PinNote(long userId, long noteId);
        public string AddImage(string imagePath, long userId, long noteId);
        public NoteEntity AddColour(long userId, long noteId, string colour);
    }
}

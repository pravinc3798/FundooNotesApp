using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long userId)
        {
            try
            {
                return noteRL.AddNotes(noteModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> ViewNotes(long userId)
        {
            try
            {
                return noteRL.ViewNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return noteRL.DeleteNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity EditNote(NoteModel noteModel, long userId, long noteId)
        {
            try
            {
                return noteRL.EditNote(noteModel, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ArchiveNote(long userId, long noteId)
        {
            try
            {
                return noteRL.ArchiveNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PinNote(long userId, long noteId)
        {
            try
            {
                return noteRL.PinNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TrashNote(long userId, long noteId)
        {
            try
            {
                return noteRL.TrashNote(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

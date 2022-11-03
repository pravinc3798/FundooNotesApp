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
    }
}

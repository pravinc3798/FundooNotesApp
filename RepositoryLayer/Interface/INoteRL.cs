﻿using CommonLayer.Model;
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
    }
}

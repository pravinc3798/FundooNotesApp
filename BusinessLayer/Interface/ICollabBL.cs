﻿using RepositoryLayer.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollaborator(long userId, long noteId, string email);
        public IEnumerable ViewCollaborators(long userId, long noteId);
    }
}

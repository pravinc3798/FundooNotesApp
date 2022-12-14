using RepositoryLayer.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollaborator(long userId, long noteId, string email);
        public IEnumerable<CollabEntity> ViewCollaborators(long userId, long noteId);
        public bool DeleteCollaborator(long userId, long noteId, string email);
    }
}

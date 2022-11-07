using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;

        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollabEntity AddCollaborator(long userId, long noteId, string email)
        {
            try
            {
                return collabRL.AddCollaborator(userId, noteId, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable ViewCollaborators(long userId, long noteId)
        {
            try
            {
                return collabRL.ViewCollaborators(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

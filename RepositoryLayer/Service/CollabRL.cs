﻿using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundoContext;

        public CollabRL(FundooContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public CollabEntity AddCollaborator(long userId, long noteId, string email)
        {
            try
            {
                var note = fundoContext.NoteTable.Where(n => n.UserId == userId && n.NoteID == noteId).FirstOrDefault();
                var emailCheck = fundoContext.UserTable.Where(e => e.Email == email).FirstOrDefault();

                if (note != null && emailCheck != null)
                {
                    CollabEntity collabEntity = new CollabEntity() { NoteId = noteId, UserId = userId, Email = email };
                    fundoContext.CollabTable.Add(collabEntity);
                    fundoContext.SaveChanges();

                    return collabEntity;
                }

                return null;
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
                var collabs = fundoContext.CollabTable.Where(c => c.UserId == userId && c.NoteId == noteId).DefaultIfEmpty();
                if (collabs != null)
                {
                    return collabs;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

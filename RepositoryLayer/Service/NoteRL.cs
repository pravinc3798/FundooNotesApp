using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundoContext;

        public NoteRL(FundooContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public NoteEntity AddNotes(NoteModel noteModel, long userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity()
                {
                    Title = noteModel.Title,
                    Archive = noteModel.Archive,
                    Color = noteModel.Color,
                    Created = DateTime.Now,
                    Description = noteModel.Description,
                    Edited = DateTime.Now,
                    Image = noteModel.Image,
                    Pin = noteModel.Pin,
                    Reminder = noteModel.Reminder,
                    Trash = noteModel.Trash,
                    UserId = userId,
                };

                fundoContext.NoteTable.Add(noteEntity);
                int result = fundoContext.SaveChanges();

                if (result != 0)
                    return noteEntity;
                else
                    return null;
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
                var allNotes = fundoContext.NoteTable.Where(n => n.UserId == userId);
                if (allNotes != null)
                {
                    return allNotes;
                }
                else
                    return null;
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
                var note = fundoContext.NoteTable.Where(n => n.NoteID == noteId).FirstOrDefault();
                if (note != null)
                {
                    fundoContext.NoteTable.Remove(note);
                    fundoContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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

        public NoteEntity EditNote(NoteModel noteModel, long userId, long noteId)
        {
            try
            {
                var note = fundoContext.NoteTable.Where(n => n.NoteID == noteId && n.UserId == userId).FirstOrDefault();

                if (note != null)
                {
                    note.Title = noteModel.Title != "string" ? noteModel.Title : note.Title;
                    note.Archive = noteModel.Archive != true ? noteModel.Archive : note.Archive;
                    note.Color = noteModel.Color != "string" ? noteModel.Color : note.Color;
                    note.Description = noteModel.Description != "string" ? noteModel.Description : note.Description;
                    note.Image = noteModel.Image != "string" ? noteModel.Image : note.Image;
                    note.Pin = noteModel.Pin != true ? noteModel.Pin : note.Pin;
                    note.Reminder = noteModel.Reminder != DateTime.UtcNow ? noteModel.Reminder : note.Reminder;
                    note.Trash = noteModel.Trash != true ? noteModel.Trash : note.Trash; ;
                    note.Edited = DateTime.Now;

                    fundoContext.NoteTable.Update(note);
                    fundoContext.SaveChanges();
                    return note;
                }
                else
                    return null;
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
                var note = fundoContext.NoteTable.Where(n => n.UserId == userId && n.NoteID == noteId).FirstOrDefault();

                if (note != null)
                {
                    note.Archive = !note.Archive;
                    fundoContext.SaveChanges();
                    return true;
                }
                return false;
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
                var note = fundoContext.NoteTable.Where(n => n.UserId == userId && n.NoteID == noteId).FirstOrDefault();

                if (note != null)
                {
                    note.Pin = !note.Pin;
                    fundoContext.SaveChanges();
                    return true;
                }
                return false;
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
                var note = fundoContext.NoteTable.Where(n => n.UserId == userId && n.NoteID == noteId).FirstOrDefault();

                if (note != null)
                {
                    note.Trash = !note.Trash;
                    fundoContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddImage(string imagePath, long userId, long noteId)
        {
            try
            {
                var note = fundoContext.NoteTable.Where(n => n.UserId == userId && n.NoteID == noteId).FirstOrDefault();

                if (note != null)
                {
                    Account account = new Account("dfhribck9", "773724862418468", "C9C6v_j8H522pFoUA91z1WWjb_8");
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams parameters = new ImageUploadParams();

                    parameters.File = new FileDescription(imagePath);
                    parameters.PublicId = userId + "_" + noteId + "_" + DateTime.Now.ToShortDateString();

                    ImageUploadResult uploadDetails = cloudinary.Upload(parameters);

                    note.Image = uploadDetails.Url.ToString();
                    fundoContext.SaveChanges();

                    return note.Image;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

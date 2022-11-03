using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundoNoteApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;

        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [HttpPost]
        [Route("AddNote")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.AddNotes(noteModel, userID);

                if (result != null)
                    return Ok(new { success = true, message = "Note Added", data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("ViewNotes")]
        public IActionResult ViewNotes()
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.ViewNotes(userID);

                if (result != null)
                    return Ok(new { success = true, message = "All notes for user : " + userID, data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

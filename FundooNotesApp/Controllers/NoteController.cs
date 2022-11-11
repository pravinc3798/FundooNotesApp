using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNoteApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;

        public NoteController(INoteBL noteBL, IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Add")]
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
        [Route("View")]
        public async Task<IActionResult> ViewNotes()
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                IEnumerable<NoteEntity> result;
                string serializedData;
                string key = "NotesForUser_" + userID.ToString();
                var encodedData = await distributedCache.GetAsync(key);

                if (encodedData != null)
                {
                    serializedData = Encoding.UTF8.GetString(encodedData);
                    result = JsonConvert.DeserializeObject<IEnumerable<NoteEntity>>(serializedData);
                }
                else
                {
                    result = noteBL.ViewNotes(userID);

                    if (result != null)
                    {
                        serializedData = JsonConvert.SerializeObject(result);
                        encodedData = Encoding.UTF8.GetBytes(serializedData);
                        var options = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10));
                        await distributedCache.SetAsync(key, encodedData, options);
                    }
                }

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

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.DeleteNote(userID, noteId);

                if (result != false)
                    return Ok(new { success = true, message = "Note Deleted" });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult EditNote(long noteId, NoteModel noteModel)
        {
            try
            {
                var userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.EditNote(noteModel, userID, noteId);

                if (result != null)
                    return Ok(new { success = true, message = "Note Edited", data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = noteBL.ArchiveNote(userId, noteId);

                if (result)
                    return Ok(new { success = true, message = "Done" });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = noteBL.PinNote(userId, noteId);

                if (result)
                    return Ok(new { success = true, message = "Done" });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = noteBL.TrashNote(userId, noteId);

                if (result)
                    return Ok(new { success = true, message = "Done" });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Image")]
        public IActionResult AddImage(long noteId, string imagePath)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = noteBL.AddImage(imagePath, userId, noteId);

                if (result != null)
                    return Ok(new { success = true, message = "Done", data = result });
                else
                    return BadRequest(new { success = false, message = "something went wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Color")]
        public IActionResult AddColour(long noteId, string colour)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = noteBL.AddColour(userId, noteId, colour);

                if (result != null)
                    return Ok(new { success = true, message = "Done", data = result });
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

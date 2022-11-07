using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNoteApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }

        [HttpPut]
        [Route("Add")]
        public IActionResult AddCollaborator(long noteId, string email)
        {
			try
			{
				var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = collabBL.AddCollaborator(userId,noteId,email);

                if (result != null)
                    return Ok(new {success = true, message = "Collaborator Added", data = result});
                else
                    return BadRequest(new {success = false, message = "Something Went Wrong"});
            }
			catch (Exception)
			{
				throw;
			}
        }

        [HttpGet]
        [Route("View")]
        public IActionResult ViewCollaborator(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = collabBL.ViewCollaborators(userId, noteId);

                if (result != null)
                    return Ok(new { success = true, data = result });
                else
                    return BadRequest(new { success = false, message = "Something Went Wrong" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

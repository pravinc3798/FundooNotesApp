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
        [Route("AddCollaborator")]
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
    }
}

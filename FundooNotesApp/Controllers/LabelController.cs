using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundoNoteApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddLabel(string name)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.AddLabel(userId, name);

                if (result != null)
                    return Ok(new { success = true, message = "Label Added", data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("Edit")]
        [HttpPut]
        public IActionResult EditLabel(long labelId, string newName)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.EditLabel(userId, labelId, newName);

                if (result != null)
                    return Ok(new { success = true, message = "Label Edited", data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.DeleteLabel(userId, labelId);

                if (result)
                    return Ok(new { success = true, message = "Label Deleted"});
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("View")]
        [HttpGet]
        public IActionResult ViewLabel()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ViewLabel(userId);

                if (result != null)
                    return Ok(new { success = true, message = "Labels for user : " + userId, data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("AddNoteLabel")]
        [HttpPost]
        public IActionResult AddLabelToNote(long noteId, long labelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.AddLabelToNote(userId, noteId, labelId);

                if (result != null)
                    return Ok(new { success = true, message = "Label Added", data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("RemoveNoteLabel")]
        [HttpDelete]
        public IActionResult RemoveNoteLabel(long noteId, long labelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.RemoveNoteLabel(userId, noteId, labelId);

                if (result)
                    return Ok(new { success = true, message = "Label Deleted" });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Route("ViewNoteLabels")]
        [HttpGet]
        public IActionResult ViewNoteLabel(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ViewLabelsForNote(userId, noteId);

                if (result != null)
                    return Ok(new { success = true, message = "Labels for note : " + noteId, data = result });
                else
                    return BadRequest(new { success = false, message = "Something went wrong" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

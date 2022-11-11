using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;

        public CollabController(ICollabBL collabBL, IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            this.collabBL = collabBL;
            this.distributedCache = distributedCache;
            this.logger = logger;
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
        public async Task<IActionResult> ViewCollaborator(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                IEnumerable<CollabEntity> result;
                string key = "CollabsForNote_" + noteId + "_User_" + userId.ToString();
                string serializedData;
                var encodedData = await distributedCache.GetAsync(key);

                if (encodedData != null)
                {
                    serializedData = Encoding.UTF8.GetString(encodedData);
                    result = JsonConvert.DeserializeObject<IEnumerable<CollabEntity>>(serializedData);
                }
                else
                {
                    result = collabBL.ViewCollaborators(userId, noteId);

                    if (result != null)
                    {
                        serializedData = JsonConvert.SerializeObject(result);
                        encodedData = Encoding.UTF8.GetBytes(serializedData);
                        var options = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(20));

                        await distributedCache.SetAsync(key, encodedData, options);
                    }
                }

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

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCollaborator(long noteId, string email)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = collabBL.DeleteCollaborator(userId, noteId, email);

                if (result)
                    return Ok(new { success = true, message = "Collaborator Deleted" });
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

using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FundoNoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<CollabController> logger;

        public UserController(IUserBL userBL, ILogger<CollabController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);

                if (result != null)
                    return Ok(new {success = true, message = "User has been registered", data = result});
                else
                    return BadRequest(new { success = false, message = "Registration Unsuccessfull"});
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var result = userBL.Login(loginModel);

                if (result != null)
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                else
                    return BadRequest(new { success = false, message = "Login Unsuccessfull" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBL.ForgetPassword(email);

                if (result != null)
                    return Ok(new { success = true, message = "email sent successfully" });
                else
                    return BadRequest(new { success = false, message = "email not sent" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != false)
                    return Ok(new { success = true, message = "password has been reset" });
                else
                    return BadRequest(new { success = false, message = "try again" });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

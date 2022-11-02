using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundoNoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
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
    }
}

using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundoContext;
        private readonly IConfiguration configuration;
        public UserRL(FundooContext fundoContext, IConfiguration configuration)
        {
            this.fundoContext = fundoContext;
            this.configuration = configuration;
        }

        private static readonly string mask = "safhajkfh28934iowqrf@#42@#$";
        private static string Encrypt(string pass)
        {
            if (pass == null) return "";

            pass += mask;

            var encodedPass = Encoding.UTF8.GetBytes(pass);
            return Convert.ToBase64String(encodedPass);
        }

        private static string Decrypt(string encodedPass)
        {
            if (encodedPass == null) return "";

            var encodedBytes = Convert.FromBase64String(encodedPass);
            var decodedPass = Encoding.UTF8.GetString(encodedBytes);
            return decodedPass.Substring(0, decodedPass.Length - mask.Length);
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity
                {
                    FirstName = userRegistrationModel.FirstName,
                    LastName = userRegistrationModel.LastName,
                    Email = userRegistrationModel.Email,
                    UserPassword = Encrypt(userRegistrationModel.UserPassword)
                };

                fundoContext.UserTable.Add(userEntity);

                int result = fundoContext.SaveChanges();

                if (result != 0)
                    return userEntity;
                else
                    return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(LoginModel loginModel)
        {
            try
            {
                //var user = fundoContext.UserTable.Where(d => d.Email == loginModel.Email && d.UserPassword == loginModel.UserPassword).Select(d => d.FirstName).FirstOrDefault();
                var userDetails = fundoContext.UserTable.Where(d => d.Email == loginModel.Email && Decrypt(d.UserPassword) == loginModel.UserPassword).FirstOrDefault();
                if (userDetails != null)
                {
                    var result = GenerateSecurityToken(userDetails.Email, userDetails.UserId);
                    return result;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string ForgetPassword(string email)
        {
            try
            {
                var emailCheck = fundoContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (emailCheck != null)
                {
                    var token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetLink(string email, string password, string confirmPassword)
        {
            try
            {
                var emailCheck = fundoContext.UserTable.FirstOrDefault(x => x.Email == email);

                if (password.Equals(confirmPassword) && emailCheck != null)
                {
                    emailCheck.UserPassword = Encrypt(password);
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

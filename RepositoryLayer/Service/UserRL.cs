﻿using CommonLayer.Model;
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

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity
                {
                    FirstName = userRegistrationModel.FirstName,
                    LastName = userRegistrationModel.LastName,
                    Email = userRegistrationModel.Email,
                    UserPassword = userRegistrationModel.UserPassword
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
                var userDetails = fundoContext.UserTable.Where(d => d.Email == loginModel.Email && d.UserPassword == loginModel.UserPassword).FirstOrDefault();
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

    }
}

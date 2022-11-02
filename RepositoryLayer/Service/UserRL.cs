using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundoContext;
        public UserRL(FundooContext fundoContext)
        {
            this.fundoContext = fundoContext;
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
                var user = fundoContext.UserTable.Where(d => d.Email == loginModel.Email && d.UserPassword == loginModel.UserPassword).Select(d => d.FirstName).FirstOrDefault();
                if (user != null)
                {
                    var result = "Logged in as : " + user;
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

    }
}

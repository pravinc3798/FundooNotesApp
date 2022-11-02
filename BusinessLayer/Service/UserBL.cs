using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity Registration(UserRegistrationModel registrationModel)
        {
			try
			{
                return userRL.Registration(registrationModel);
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}

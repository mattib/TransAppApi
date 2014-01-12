using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TransAppApi.Entities;
using TransAppApi.Managment;
using TransAppApi.Models;
using TransAppApi.SearchQueries;

namespace TransAppApi.Controllers
{
    public class UserController : ApiController
    {
        private IUserManager m_userManager;

        public UserController()
        {
            m_userManager = new UserManager();
        }

        // GET api/user
        public IEnumerable<User> Get()
        {
            var result = m_userManager.GetEntities(new UsersSearchQuery());
            return result;
        }

        // GET api/user/{id}
        public User Get(int id)
        {
            var result = m_userManager.GetEntity(id);
            return result;
        }

        // GET api/user/?userName={userName}
        public User GetByUserName(string userName)
        {
            var result = m_userManager.GetUser(userName);
            return result;
        }

        // GET api/user/?comapnyId={comapnyId}
        public IEnumerable<TransAppApi.Entities.User> GetByCompanyId(int comapnyId)
        {
            var usersSearchQuery = new UsersSearchQuery();
            usersSearchQuery.CompanyId = comapnyId;

            var result = m_userManager.GetEntities(usersSearchQuery);
            return result;
        }

        // POST api/user - ?
        public void Post(TransAppApi.Entities.User value)
        {
            if (value == null)
            {
                throw new NullReferenceException("No user was recived");
            }
            m_userManager.SaveEntity(new [] {value});
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
            m_userManager.DeleteEntity(id);
        }


        //To be changed in the future
        [HttpGet]
        public int AuthenticateUser(string userName, string password)
        {
            var userManager = m_userManager as UserManager;
            return userManager.AuthenticateUser(userName, password);
        }

        [HttpGet]
        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var userManager = m_userManager as UserManager;
            return userManager.ChangePassword(userId, oldPassword, newPassword);
        }

        [HttpGet]
        public int CreateUser(string userName, string password, int companyId)
        {
            var userManager = m_userManager as UserManager;
            return userManager.CreateUser(userName, password, companyId);
        }
    }
}

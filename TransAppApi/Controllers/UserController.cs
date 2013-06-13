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
        public IEnumerable<User> GetByCompanyId(int comapnyId)
        {
            var usersSearchQuery = new UsersSearchQuery();
            usersSearchQuery.CompanyId = comapnyId;

            var result = m_userManager.GetEntities(usersSearchQuery);
            return result;
        }

        // POST api/user - ?
        public void Post(User value) // [FromBody]string value
        {
            m_userManager.SaveEntity(new [] {value});
        }

        //// PUT api/user/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/user/5
        public void Delete(int id)
        {
            m_userManager.DeleteEntity(id);
        }

        public bool AuthenticateUser(string userName, string password)
        {
            return true;
            // m_userManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

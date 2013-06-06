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

        // GET api/event
        public IEnumerable<User> Get()
        {
            var result = m_userManager.GetEntities(new EntitiesSearchQuery());
            return result;
        }

        // GET api/event/5
        public User Get(int id)
        {
            var result = m_userManager.GetEntity(id);
            return result;
        }
        // GET api/event/?userName={userName}
        public User GetByUserName(string userName)
        {
            var result = m_userManager.GetUser(userName);
            return result;
        }

        // POST api/event - ?
        public void Post(User value) // [FromBody]string value
        {
            m_userManager.SaveEntity(new [] {value});
        }

        //// PUT api/event/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_userManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

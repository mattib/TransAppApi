using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TransAppApi.DataSources;
using TransAppApi.Models;

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
            var result = m_userManager.GetUsers();
            return result;
        }

        // GET api/event/5
        public User Get(int id)
        {
            var result = m_userManager.GetUser(id);
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
            m_userManager.SaveUser(value);
        }

        //// PUT api/event/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_userManager.DeleteUser(id);
        }
    }
}

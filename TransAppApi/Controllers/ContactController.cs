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
    public class ContactController : ApiController
    {
        private IEntityManager<Contact> m_contactManager;

        public ContactController()
        {
            m_contactManager = new ContactManager();
        }

        // GET api/event
        public IEnumerable<Contact> Get()
        {
            var result = m_contactManager.GetEntities(new EntitiesSearchQuery() );
            return result;
        }

        // GET api/event/5
        public Contact Get(int id)
        {
            var result = m_contactManager.GetEntity(id);
            return result;
        }
        //// GET api/event/?userName={userName}
        //public User GetByUserName(string userName)
        //{
        //    var result = m_contactManager.GetUser(userName);
        //    return result;
        //}

        // POST api/event - ?
        public void Post(Contact value) // [FromBody]string value
        {
            m_contactManager.SaveEntity(new [] {value});
        }

        //// PUT api/event/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_contactManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

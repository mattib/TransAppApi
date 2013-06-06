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
    public class CustomerController : ApiController
    {
        private IEntityManager<Customer> m_customerManager;

        public CustomerController()
        {
            m_customerManager = new CustomerManager();
        }

        // GET api/event
        public IEnumerable<Customer> Get()
        {
            var result = m_customerManager.GetEntities(new EntitiesSearchQuery() );
            return result;
        }

        // GET api/event/5
        public Customer Get(int id)
        {
            var result = m_customerManager.GetEntity(id);
            return result;
        }
        //// GET api/event/?userName={userName}
        //public User GetByUserName(string userName)
        //{
        //    var result = m_addressManager.GetUser(userName);
        //    return result;
        //}

        // POST api/event - ?
        public void Post(Customer value) // [FromBody]string value
        {
            m_customerManager.SaveEntity(new [] {value});
        }

        //// PUT api/event/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_customerManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

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
    public class AddressController : ApiController
    {
        private IEntityManager<Address> m_addressManager;

        public AddressController()
        {
            m_addressManager = new AddressManager();
        }

        // GET api/event
        public IEnumerable<Address> Get()
        {
            var result = m_addressManager.GetEntities(new EntitiesSearchQuery() );
            return result;
        }

        // GET api/event/5
        public Address Get(int id)
        {
            var result = m_addressManager.GetEntity(id);
            return result;
        }
        //// GET api/user/?comapnyId={comapnyId}
        //public IEnumerable<User> GetByCompanyId(int comapnyId)
        //{
        //    var usersSearchQuery = new UsersSearchQuery();
        //    usersSearchQuery.CompanyId = comapnyId;

        //    var result = m_userManager.GetEntities(usersSearchQuery);
        //    return result;
        //}

        // POST api/event - ?
        public void Post(Address value) // [FromBody]string value
        {
            m_addressManager.SaveEntity(new [] {value});
        }

        //// PUT api/event/5  -- ?
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_addressManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

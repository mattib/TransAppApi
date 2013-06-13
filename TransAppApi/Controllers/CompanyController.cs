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
    public class CompanyController : ApiController
    {
        private IEntityManager<Company> m_comapanyManager;

        public CompanyController()
        {
            m_comapanyManager = new CompanyManager();
        }

        // GET api/event
        public IEnumerable<Company> Get()
        {
            var result = m_comapanyManager.GetEntities(new EntitiesSearchQuery() );
            return result;
        }

        // GET api/event/5
        public Company Get(int id)
        {
            var result = m_comapanyManager.GetEntity(id);
            return result;
        }

        // POST api/event - ?
        public void Post(Company value) // [FromBody]string value
        {
            m_comapanyManager.SaveEntity(new [] {value});
        }

        // DELETE api/event/5
        public void Delete(int id)
        {
            m_comapanyManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

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
    public class TaskController : ApiController
    {
        private IEntityManager<Task> m_taskManager;

        public TaskController()
        {
            m_taskManager = new TaskManager();
        }

        // GET api/Task
        public IEnumerable<Task> Get()
        {
            var result = m_taskManager.GetEntities(new TasksSearchQuery());
            return result;
        }

        // GET api/Task/{id}
        public Task Get(int id)
        {
            var result = m_taskManager.GetEntity(id);
            return result;
        }

        // GET api/Task?userId={userId}&lastModified={lastModified}
        public IEnumerable<Task> GetByUserId(int userId, DateTime? lastModified)
        {
            var tasksSearchQuery = new TasksSearchQuery();
            tasksSearchQuery.UserId = userId;
            tasksSearchQuery.LastModified = lastModified;
            tasksSearchQuery.RowStatus = 0;
            var result = m_taskManager.GetEntities(tasksSearchQuery);
            return result;
        }

        // GET api/Task?companyId={companyId}&lastModified={lastModified}
        public IEnumerable<Task> GetByCompanyId(int companyId, DateTime? lastModified)
        {
            var tasksSearchQuery = new TasksSearchQuery();
            tasksSearchQuery.CompanyId = companyId;
            tasksSearchQuery.LastModified = lastModified;
            tasksSearchQuery.RowStatus = 0;
            var result = m_taskManager.GetEntities(tasksSearchQuery);
            return result;
        }

        // GET api/Task?deliveryNumber={deliveryNumber}
        public Task GetByDeliveryNumber(string deliveryNumber)
        {
            var tasksSearchQuery = new TasksSearchQuery();
            tasksSearchQuery.DeliveryNumber = deliveryNumber;
            var result = m_taskManager.GetEntities(tasksSearchQuery);
            return result.FirstOrDefault();
        }

        // POST api/Task
        public void Post(Task value) // [FromBody]string value
        {
            m_taskManager.SaveEntity(new [] {value});
        }

        // DELETE api/Task/{id}
        public void Delete(int id)
        {
            m_taskManager.DeleteEntity(id);
        }

        // authneticate

        //change password

        //
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.DataSources;
using TransAppApi.Entities;
using TransAppApi.Models;
using TransAppApi.SearchQueries;

namespace TransAppApi.Managment
{
    public class TaskManager : IEntityManager<Task>
    {
        private MongoDbTasksDataSource m_tasksDataSource;

        public TaskManager()
        {
            m_tasksDataSource = new MongoDbTasksDataSource();
        }

        public Task[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<Task>();

            var tasksList = QueryEvents(searchQuery);

            foreach (var mongoDbTask in tasksList)
            {
                var task = new Task(mongoDbTask);
                result.Add(task);
            }

            return result.ToArray();
        }

        public Task GetEntity(int id)
        {
            var mongoDbtask = m_tasksDataSource.GetTask(id);

            var result = new Task(mongoDbtask);

            return result;
        }

        public void SaveEntity(Task[] tasks)
        {
            foreach (var task in tasks)
            {
                var mongoDbTask = new MongoDbTask(task);
                m_tasksDataSource.SaveTask(mongoDbTask);
            }
        }

        public void DeleteEntity(int id)
        {
            m_tasksDataSource.DeleteTask(id);
        }

        private IEnumerable<Task> QueryEvents(EntitiesSearchQuery entitiesSearchQuery)
        {
            var tasksList = new List<Task>();

            var tasksSearchQuery = entitiesSearchQuery as TasksSearchQuery;

            IEnumerable<Task> tasksDataSource = m_tasksDataSource.GetAll();

            tasksDataSource = FilterUserId(tasksSearchQuery, tasksDataSource);

            tasksDataSource = FilterCompanyId(tasksSearchQuery, tasksDataSource);

            tasksDataSource = FilterRowStatus(tasksSearchQuery, tasksDataSource);

            tasksDataSource = FilterTime(tasksSearchQuery, tasksDataSource);

            tasksDataSource = FilterDeliveryNumber(tasksSearchQuery, tasksDataSource);
            return tasksDataSource;
        }

        private IEnumerable<Task> FilterUserId(TasksSearchQuery tasksSearchQuery, IEnumerable<Task> tasks)
        {
            if (tasksSearchQuery.UserId.HasValue)
            {
                tasks = tasks.Where(item => (item.UserId == tasksSearchQuery.UserId.Value));
            }
            return tasks;
        }

        private IEnumerable<Task> FilterCompanyId(TasksSearchQuery tasksSearchQuery, IEnumerable<Task> tasks)
        {
            if (tasksSearchQuery.CompanyId.HasValue)
            {
                tasks = tasks.Where(item => (item.CompanyId == tasksSearchQuery.CompanyId.Value));
            }
            return tasks;
        }

        private IEnumerable<Task> FilterRowStatus(TasksSearchQuery tasksSearchQuery, IEnumerable<Task> tasks)
        {
            if (tasksSearchQuery.RowStatus.HasValue)
            {
                tasks = tasks.Where(item => (item.RowStatus == tasksSearchQuery.RowStatus.Value));
            }
            return tasks;
        }

        private IEnumerable<Task> FilterTime(TasksSearchQuery tasksSearchQuery, IEnumerable<Task> tasks)
        {
            if (tasksSearchQuery.LastModified.HasValue)
            {
                tasks = tasks.Where(item => (item.LastModified >= tasksSearchQuery.LastModified.Value));
            }
            return tasks;
        }

        private IEnumerable<Task> FilterDeliveryNumber(TasksSearchQuery tasksSearchQuery, IEnumerable<Task> tasks)
        {
            if (!string.IsNullOrEmpty(tasksSearchQuery.DeliveryNumber))
            {
                tasks = tasks.Where(item => (item.DeliveryNumber.Equals(tasksSearchQuery.DeliveryNumber)));
            }
            return tasks;
        }
    }
}
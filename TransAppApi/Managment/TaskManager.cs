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

            foreach (var task in tasksList)
            {
                //var task = new Task(mongoDbTask);
                result.Add(task);
            }

            return result.ToArray();
        }

        public Task GetEntity(int id)
        {
            var result = m_tasksDataSource.GetTask(id);

            //var result = new Task(mongoDbtask);

            return result;
        }

        public void SaveEntity(Task[] tasks)
        {
            foreach (var task in tasks)
            {
                if (TaskIsValid(task))
                {
                    task.LastModified = DateTime.UtcNow;
                    m_tasksDataSource.SaveTask(task);

                    if ((TaskStatus)task.TaskStatus == TaskStatus.Created)
                    {
                        var eventManager = new EventManager();


                        var events = new List<Event>();

                        events.Add(CreateTaskCreationEvent(task));
                        events.Add(CreateAssignTaskEvent(task));

                        eventManager.SaveEvents(events.ToArray());
                    }
                }
                else
                {
                    throw new ArgumentNullException("The Creation of Task failed.");
                }
            }
        }

        private static Event CreateTaskCreationEvent(Task task)
        {
            return CreateEvent(task, TaskStatus.Created);
        }

        private static Event CreateAssignTaskEvent(Task task)
        {
            return CreateEvent(task, TaskStatus.Assigned);
        }

        private static Event CreateEvent(Task task, TaskStatus taskStatus)
        {
            var eventItem = new Event();
            eventItem.Id = 0;
            eventItem.TaskId = task.Id;
            eventItem.Time = DateTime.UtcNow;
            eventItem.EventType = (int)InputType.TaskStatusChange;
            eventItem.RowStatus = 0;
            eventItem.InputType = (int)taskStatus;
            eventItem.UserId = task.UserId;
            return eventItem;
        }

        private bool TaskIsValid(Task task)
        {
            var result = true;

            if (task.Id != 0)
            {
                var currentTask = GetEntity(task.Id);
                if (task.RowStatus != 0
                    || task.TaskStatus == (int)TaskStatus.Finished
                    || task.TaskStatus == (int)TaskStatus.Canceled)
                {
                    result = false;
                }
            }

            if (!UserIsValid(task.UserId))
            {
                result = false;
            }

            //if (!CompanyIsValid(task.CompanyId))
            //{
            //    result = false;
            //}

            //if (!AddressIsValid(task.SenderAddress.Id))
            //{
            //    result = false;
            //}

            //if (!AddressIsValid(task.ReciverAddress.Id))
            //{
            //    result = false;
            //}

            return result;
        }

        private bool UserIsValid(int userId)
        {
            var userManager = new UserManager();
            var result = userManager.EntityExists(userId);
           
            return result;
        }

        //private bool CompanyIsValid(int comapanyId)
        //{
        //    var companyManager = new CompanyManager();
        //    var result = companyManager.EntityExists(comapanyId);

        //    return result;
        //}

        //private bool AddressIsValid(int addressId)
        //{
        //    var addressManager = new AddressManager();
        //    var result = addressManager.EntityExists(addressId);

        //    return result;
        //}

        public void DeleteEntity(int id)
        {
            m_tasksDataSource.DeleteTask(id);
        }

        public bool EntityExists(int id)
        {
            var result = m_tasksDataSource.HasTask(id);
            return result;
        }

        private IEnumerable<Task> QueryEvents(EntitiesSearchQuery entitiesSearchQuery)
        {
            var tasksList = new List<Task>();

            var tasksSearchQuery = entitiesSearchQuery as TasksSearchQuery;

            IEnumerable<Task> tasksDataSource = m_tasksDataSource.GetTasks(tasksSearchQuery);

            // TO DO in the future I have to make this more afficent too.
            tasksDataSource = FilterUserId(tasksSearchQuery, tasksDataSource);

            tasksDataSource = FilterCompanyId(tasksSearchQuery, tasksDataSource);

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
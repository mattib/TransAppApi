using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;
using TransAppApi.SearchQueries;
using MongoDB.Driver.Linq;

namespace TransAppApi.DataSources
{
    public class MongoDbTasksDataSource : MongoDbDataSource, ITasksDataSource
    {
        private readonly string m_tasksDataBaseName = "tasks";

        private MongoCollection<MongoDbTask> GetTasksCollection()
        {
            var collections = DbManager.GetCollection<MongoDbTask>(m_tasksDataBaseName);

            return collections;
        }

        public Task[] GetTasks(TasksSearchQuery taskSearchQuery)
        {
            var tasksCollection = GetTasksCollection();

            var rowStatus = taskSearchQuery.RowStatus.HasValue? taskSearchQuery.RowStatus.Value : 0;
            var lastModified = taskSearchQuery.LastModified.HasValue? taskSearchQuery.LastModified.Value : DateTime.UtcNow.AddDays(-7);

            //tasksCollection.RemoveAll();
            var tasks = from task in tasksCollection.AsQueryable<MongoDbTask>()
                        where task.RowStatus == rowStatus && task.LastModified >= lastModified
                         select task;

            return ToTasksArray(tasks);
        }

        public bool HasTask(int id)
        {
            var tasksCollection = GetTasksCollection();
            var result = tasksCollection.AsQueryable<MongoDbTask>().Where(task => task.Id == id).Any();
            return result;
        }

        public Task GetTask(int id)
        {
            var tasksCollection = GetTasksCollection();

            var task = tasksCollection.FindOneById(id);

            var result = task != null ? ToTask(task) : null;

            return result;
        }

        public void SaveTask(Task task)
        {
            if (task.Id == 0)
            {
                task.Id = NewId();
                task.Created = DateTime.Now;
            }

            var mongoDbTask = new MongoDbTask(task);
            mongoDbTask.LastModified = DateTime.UtcNow;
            
            var tasksCollection = GetTasksCollection();
            tasksCollection.Save(mongoDbTask);
        }

        public void DeleteTask(int id)
        {
            var task = GetTask(id);
            task.RowStatus = 1;
            task.LastModified = DateTime.UtcNow;
            var tasksCollection = GetTasksCollection();
            tasksCollection.Save(task);
        }

        public int NewId()
        {
            var tasksCollection = GetTasksCollection();
            var amountOfTasks = (int)tasksCollection.Count();

            return amountOfTasks + 1;
        }

        private Task[] ToTasksArray(IEnumerable<MongoDbTask> mongoDbTasks)
        {
            var result = mongoDbTasks.Select(ToTask);
            return result.ToArray();
        }

        private Task ToTask(MongoDbTask mongoDbTask)
        {
            var task = new Task();
            task.Id = mongoDbTask.Id;
            task.DeliveryNumber = mongoDbTask.DeliveryNumber;

            task.UserId = mongoDbTask.UserId;
            task.CompanyId = mongoDbTask.CompanyId;

            task.SenderAddress = mongoDbTask.SenderAddress;
            task.ReciverAddress = mongoDbTask.ReciverAddress;

            task.TaskStatus = mongoDbTask.TaskStatus;
            task.Created = mongoDbTask.Created;
            task.PickedUpAt = mongoDbTask.PickedUpAt;
            task.DeliveredAt = mongoDbTask.DeliveredAt;
            task.PickUpTime = mongoDbTask.PickUpTime;
            task.DeliveryTime = mongoDbTask.DeliveryTime;
            task.LastModified = mongoDbTask.LastModified;
            task.ReciverComment = mongoDbTask.ReciverComment;
            task.SenderComment = mongoDbTask.SenderComment;

            //task.Contact = new Contact(contact);
            //task.Contact.Company = task.Company;

            task.RowStatus = mongoDbTask.RowStatus;
            task.TaskType = mongoDbTask.TaskType;
            task.DataExtention = mongoDbTask.DataExtention;
            task.SignatureId = mongoDbTask.SignatureId;
            if (!string.IsNullOrEmpty(mongoDbTask.ImageId))
            {
                task.ImageId = mongoDbTask.ImageId.Split(',');
            }
            task.Rejected = mongoDbTask.Rejected;
            
            return task;
        }

        //private static User GetUser(MongoDbTask mongoDbTask)
        //{
        //    var result = default(User);
        //    var mongoDbUsersDataSource = new MongoDbUsersDataSource();

        //    result = mongoDbUsersDataSource.GetUser(mongoDbTask.UserId);
        //    return result;
        //}

        //private static Address GetAddress(int? addressId)
        //{
        //    var result = default(Address);
        //    if (addressId.HasValue)
        //    {
        //        var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();

        //        result = mongoDbAddressesDataSource.GetAddress(addressId.Value);
        //    }
        //    return result;
        //}

        //private static Contact GetContact(MongoDbTask mongoDbTask)
        //{
        //    var result = default(Contact);
        //    var mongoDbContactsDataSource = new MongoDbContactsDataSource();

        //    result = mongoDbContactsDataSource.GetContact(mongoDbTask.ContactId);
        //    return result;
        //}

        //private static Company GetCompany(MongoDbTask mongoDbTask)
        //{
        //    var result = default(Company);
        //    var mongoDbCompaniesDataSource = new MongoDbCompaniesDataSource();

        //    result = mongoDbCompaniesDataSource.GetCompany(mongoDbTask.CompanyId);
        //    return result;
        //}
    }
}
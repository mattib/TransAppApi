using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;
using TransAppApi.SearchQueries;

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

        public Task[] GetAll()
        {
            var tasksCollection = GetTasksCollection();
            var query = Query<MongoDbTask>.Where(e => e.RowStatus != 1);
            var tasks = tasksCollection.Find(query);

            return ToTasksArray(tasks);
        }

        private bool QueryTask(MongoDbTask mongoDbTask, TasksSearchQuery taskSearchQuery)
        {
            var result = false;
            if (mongoDbTask.LastModified >= taskSearchQuery.LastModified)
            {
                result = true;
            }
            if (result && mongoDbTask.RowStatus == 1)
            {
                result = false;
            }

            return result;
        }

        public Task GetTask(int id)
        {
            var tasksCollection = GetTasksCollection();
            var query = Query<MongoDbTask>.EQ(e => e.Id, id);
            var task = tasksCollection.FindOne(query);

            return ToTask(task);
        }

        public void SaveTask(Task task)
        {
            if (task.Id == 0)
            {
                task.Id = NewId();
                task.Created = DateTime.Now;
            }

            var mongoDbTask = new MongoDbTask(task);
            mongoDbTask.LastModified = DateTime.Now;
            
            var tasksCollection = GetTasksCollection();
            tasksCollection.Save(mongoDbTask);
        }

        public void DeleteTask(int id)
        {
            var task = GetTask(id);
            task.RowStatus = 1;
            task.LastModified = DateTime.Now;
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

            var user = GetUser(mongoDbTask);
            task.User = new User(user);

            var company = GetCompany(mongoDbTask);
            task.Company = new Company(company);

            var senderAddress = GetAddress(mongoDbTask.SenderAddressId);
            task.SenderAddress = new Address(senderAddress);

            var reciverAddress = GetAddress(mongoDbTask.ReciverAddressId);
            task.ReciverAddress = new Address(reciverAddress);

            task.TaskStatus = mongoDbTask.TaskStatus;
            task.Created = mongoDbTask.Created;
            task.PickedUpAt = mongoDbTask.PickedUpAt;
            task.DeliveredAt = mongoDbTask.DeliveredAt;
            task.PickUpTime = mongoDbTask.PickUpTime;
            task.DeliveryTime = mongoDbTask.DeliveryTime;
            task.LastModified = mongoDbTask.LastModified;
            task.Comment = mongoDbTask.Comment;

            var contact = GetContact(mongoDbTask);
            task.Contact = new Contact(contact);
            task.Contact.Company = task.Company;

            task.RowStatus = mongoDbTask.RowStatus;
            task.TaskType = mongoDbTask.TaskType;
            task.DataExtention = mongoDbTask.DataExtention;
            task.SignatureId = mongoDbTask.SignatureId;
            task.ImageId = mongoDbTask.ImageId;
            task.UserComment = mongoDbTask.UserComment;
            task.Rejected = mongoDbTask.Rejected;
            

            return task;
        }

        private static User GetUser(MongoDbTask mongoDbTask)
        {
            var result = default(User);
            var mongoDbUsersDataSource = new MongoDbUsersDataSource();

            result = mongoDbUsersDataSource.GetUser(mongoDbTask.UserId);
            return result;
        }

        private static Address GetAddress(int? addressId)
        {
            var result = default(Address);
            if (addressId.HasValue)
            {
                var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();

                result = mongoDbAddressesDataSource.GetAddress(addressId.Value);
            }
            return result;
        }

        private static Contact GetContact(MongoDbTask mongoDbTask)
        {
            var result = default(Contact);
            var mongoDbContactsDataSource = new MongoDbContactsDataSource();

            result = mongoDbContactsDataSource.GetContact(mongoDbTask.ContactId);
            return result;
        }

        private static Company GetCompany(MongoDbTask mongoDbTask)
        {
            var result = default(Company);
            var mongoDbCompaniesDataSource = new MongoDbCompaniesDataSource();

            result = mongoDbCompaniesDataSource.GetCompany(mongoDbTask.CompanyId);
            return result;
        }
    }
}
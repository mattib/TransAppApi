using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;

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

            return tasks.ToArray();
        }

        public Task GetTask(int id)
        {
            var tasksCollection = GetTasksCollection();
            var query = Query<MongoDbTask>.EQ(e => e.Id, id);
            var task = tasksCollection.FindOne(query);

            return task;
        }

        //public Company GetUser(string userName)
        //{
        //    var comapniesCollection = GetContactsCollection();
        //    var query = Query<MongoDbCompany>.EQ(e => e.UserName, userName);
        //    var contact = comapniesCollection.FindOne(query);

        //    return contact;
        //}

        public void SaveTask(Task task)
        {
            if (task.Id == 0)
            {
                task.Id = NewId();
            }

            var mongoDbTask = new MongoDbTask(task);

            var tasksCollection = GetTasksCollection();
            tasksCollection.Save(mongoDbTask);
        }

        public void DeleteTask(int id)
        {
            var task = GetTask(id);
            task.RowStatus = 1;
            var tasksCollection = GetTasksCollection();
            tasksCollection.Save(task);
        }

        public int NewId()
        {
            var tasks = GetAll();
            var result = 0;
            foreach (var task in tasks)
            {
                var mongoDbTask = (MongoDbTask)task;
                if (mongoDbTask.MongoId.Pid > result)
                {
                    result = mongoDbTask.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
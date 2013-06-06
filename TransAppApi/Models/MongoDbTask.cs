using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbTask : Task
    {
        private ObjectId m_mongoId;

        public MongoDbTask()
        {
        }

        public MongoDbTask(Task task)
            : base(task)
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(Id.ToString()), 0);
        }

        public ObjectId MongoId
        {
            get { return m_mongoId; }
            set
            {
                m_mongoId = value;
                Id = (int)value.Pid;
            }
        }
    }
}
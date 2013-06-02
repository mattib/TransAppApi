using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Models
{
    public class MongoDbUser : User
    {
        private int m_id;
        private ObjectId m_mongoId;

        public MongoDbUser()
        {
        }

        public MongoDbUser(User user)
            : base(user)
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
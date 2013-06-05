using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Models
{
    public class MongoDbItem
    {
        public MongoDbItem()
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(Id.ToString()), 0);
        }

        private int m_id;
        private ObjectId m_mongoId;

        public ObjectId MongoId
        {
            get { return m_mongoId; }
            set
            {
                m_mongoId = value;
                Id = (int)value.Pid;
            }
        }

        public int Id
        { get { return m_id; } set { m_id = value; } }
    }
}
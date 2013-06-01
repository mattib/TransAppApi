using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.DataSources
{
    public class MongoDbDataSource
    {
        private MongoDbManager m_dbManager;


        public MongoDbManager DbManager { get { return m_dbManager; } }

        public MongoDbDataSource()
        {
            m_dbManager = new MongoDbManager();
        }
    }
}
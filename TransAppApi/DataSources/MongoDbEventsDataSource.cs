using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public class MongoDbEventsDataSource : MongoDbDataSource, IEventsDataSource
    {
        private readonly string m_eventsDataBaseName = "events";

        private MongoCollection<MongoDbEvent> GetEventsCollection()
        {
            var collections = DbManager.GetEventsCollection<MongoDbEvent>(m_eventsDataBaseName);

            return collections;
        }

        public Event[] GetAll()
        {
            var eventsCollection = GetEventsCollection();
            var query = Query<MongoDbEvent>.Where(e => e.RowStatus != 1);
            var events = eventsCollection.Find(query);

            return events.ToArray();
        }

        public Event GetEvent(int id)
        {
            var eventsCollection = GetEventsCollection();
            var query = Query<MongoDbEvent>.EQ(e => e.Id, id);
            var eventItem = eventsCollection.FindOne(query);

            return eventItem;
        }

        public void SaveEvent(Event eventItem)
        {
            if (eventItem.Id == 0)
            {
                eventItem.Id = NewId();
            }

            var MongoDbEvent = new MongoDbEvent(eventItem);

            var eventsCollection = GetEventsCollection();
            eventsCollection.Save(MongoDbEvent);
        }

        public void DeleteEvent(int id)
        {
            var eventItem = GetEvent(id);
            eventItem.RowStatus = 1;
            var eventsCollection = GetEventsCollection();
            eventsCollection.Save(eventItem);
        }

        public int NewId()
        {
            var events = GetAll();
            var result = 0;
            foreach (var item in events)
            {
                var MongoDbEvent = (MongoDbEvent)item;
                if (MongoDbEvent.MongoId.Pid > result)
                {
                    result = MongoDbEvent.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
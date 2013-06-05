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
    public class EventManager : IEventManager
    {
        private MongoDbEventsDataSource m_eventDataSource;

        public EventManager()
        {
            m_eventDataSource = new MongoDbEventsDataSource();
        }

        public Event[] GetEvents(EventSearchQuery eventSearchQuery)
        {
            var result = new List<Event>();

            var eventsList = QueryEvents(eventSearchQuery);

            foreach (var mongoEvent in eventsList)
            {
                var eventItem = new Event(mongoEvent);
                result.Add(eventItem);
            }

            return result.ToArray();
        }

        public Event GetEvent(int id)
        {
            var mongoEvent = m_eventDataSource.GetEvent(id);

            var result = new Event(mongoEvent);

            return result;
        }

        public Event[] GetUserEvents(int userId, DateTime? lastModified = null)
        {
            throw new NotImplementedException();
        }

        public void SaveEvents(Event[] events)
        {
            foreach (var eventItem in events)
            {
                var mongoEvent = new MongoDbEvent(eventItem);
                m_eventDataSource.SaveEvent(mongoEvent);
            }
        }

        public void DeleteEvent(int id)
        {
            m_eventDataSource.DeleteEvent(id);
        }

        private IEnumerable<Event> QueryEvents(EventSearchQuery eventSearchQuery)
        {
            var eventsList = new List<Event>();

            IEnumerable<Event> eventsDataSource = m_eventDataSource.GetAll();

            eventsDataSource = FilterUserId(eventSearchQuery, eventsDataSource);

            eventsDataSource = FilterRowStatus(eventSearchQuery, eventsDataSource);

            eventsDataSource = FilterTaskId(eventSearchQuery, eventsDataSource);

            eventsDataSource = FilterTime(eventSearchQuery, eventsDataSource);
            return eventsDataSource;
        }

        private IEnumerable<Event> FilterUserId(EventSearchQuery eventSearchQuery, IEnumerable<Event> events)
        {
            if (eventSearchQuery.UserId.HasValue)
            {
                events = events.Where(item => (item.UserId == eventSearchQuery.UserId.Value));
            }
            return events;
        }

        private IEnumerable<Event> FilterRowStatus(EventSearchQuery eventSearchQuery, IEnumerable<Event> events)
        {
            if (eventSearchQuery.RowStatus.HasValue)
            {
                events = events.Where(item => (item.RowStatus == eventSearchQuery.RowStatus.Value));
            }
            return events;
        }

        private IEnumerable<Event> FilterTaskId(EventSearchQuery eventSearchQuery, IEnumerable<Event> events)
        {
            if (eventSearchQuery.TaskId.HasValue)
            {
                events = events.Where(item => (item.TaskId == eventSearchQuery.TaskId.Value));
            }
            return events;
        }

        private IEnumerable<Event> FilterTime(EventSearchQuery eventSearchQuery, IEnumerable<Event> events)
        {
            if (eventSearchQuery.LastModified.HasValue)
            {
                events = events.Where(item => (item.Time >= eventSearchQuery.LastModified.Value));
            }
            return events;
        }
    }
}
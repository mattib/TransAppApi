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
    public class ContactManager : IEntityManager<Contact>
    {
        private MongoDbContactsDataSource m_contactsDataSource;

        public ContactManager()
        {
            m_contactsDataSource = new MongoDbContactsDataSource();
        }

        public Contact[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<Contact>();

            var companiesList = QueryEvents(searchQuery);

            foreach (var mongoEvent in companiesList)
            {
                var company = new Contact(mongoEvent);
                result.Add(company);
            }

            return result.ToArray();
        }

        public Contact GetEntity(int id)
        {
            var mongoEvent = m_contactsDataSource.GetContact(id);

            var result = new Contact(mongoEvent);

            return result;
        }

        public void SaveEntity(Contact[] tasks)
        {
            foreach (var contact in tasks)
            {
                m_contactsDataSource.SaveContact(contact);
            }
        }

        public void DeleteEntity(int id)
        {
            m_contactsDataSource.DeleteContact(id);
        }

        public bool EntityExists(int id)
        {
            var result = m_contactsDataSource.GetContact(id) != null;
            return result;
        }

        private IEnumerable<Contact> QueryEvents(EntitiesSearchQuery eventSearchQuery)
        {
            var eventsList = new List<Contact>();

            IEnumerable<Contact> eventsDataSource = m_contactsDataSource.GetAll();

            //eventsDataSource = FilterUserId(eventSearchQuery, eventsDataSource);

            //eventsDataSource = FilterRowStatus(eventSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTaskId(eventSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTime(eventSearchQuery, eventsDataSource);
            return eventsDataSource;
        }

        //private IEnumerable<Event> FilterUserId(EventSearchQuery eventSearchQuery, IEnumerable<Company> events)
        //{
        //    if (eventSearchQuery.UserId.HasValue)
        //    {
        //        events = events.Where(item => (item.UserId == eventSearchQuery.UserId.Value));
        //    }
        //    return events;
        //}

        //private IEnumerable<Event> FilterRowStatus(EventSearchQuery eventSearchQuery, IEnumerable<Company> events)
        //{
        //    if (eventSearchQuery.RowStatus.HasValue)
        //    {
        //        events = events.Where(item => (item.RowStatus == eventSearchQuery.RowStatus.Value));
        //    }
        //    return events;
        //}

        //private IEnumerable<Event> FilterTaskId(EventSearchQuery eventSearchQuery, IEnumerable<Company> events)
        //{
        //    if (eventSearchQuery.TaskId.HasValue)
        //    {
        //        events = events.Where(item => (item.TaskId == eventSearchQuery.TaskId.Value));
        //    }
        //    return events;
        //}

        //private IEnumerable<Event> FilterTime(EventSearchQuery eventSearchQuery, IEnumerable<Company> events)
        //{
        //    if (eventSearchQuery.LastModified.HasValue)
        //    {
        //        events = events.Where(item => (item.Time >= eventSearchQuery.LastModified.Value));
        //    }
        //    return events;
        //}
    }
}
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
    public class CompanyManager : IEntityManager<Company>
    {
        private MongoDbCompaniesDataSource m_companiesDataSource;

        public CompanyManager()
        {
            m_companiesDataSource = new MongoDbCompaniesDataSource();
        }

        public Company[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<Company>();

            var companiesList = QueryEvents(searchQuery);

            foreach (var mongoEvent in companiesList)
            {
                var company = new Company(mongoEvent);
                result.Add(company);
            }

            return result.ToArray();
        }

        public Company GetEntity(int id)
        {
            var mongoEvent = m_companiesDataSource.GetCompany(id);

            var result = new Company(mongoEvent);

            return result;
        }

        public void SaveEntity(Company[] companies)
        {
            foreach (var company in companies)
            {
                var mongoCompany = new MongoDbCompany(company);
                m_companiesDataSource.SaveCompany(mongoCompany);
            }
        }

        public void DeleteEntity(int id)
        {
            m_companiesDataSource.DeleteCompany(id);
        }

        private IEnumerable<Company> QueryEvents(EntitiesSearchQuery eventSearchQuery)
        {
            var eventsList = new List<Company>();

            IEnumerable<Company> eventsDataSource = m_companiesDataSource.GetAll();

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
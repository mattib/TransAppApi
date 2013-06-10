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

        public void SaveEntity(Company[] tasks)
        {
            foreach (var company in tasks)
            {
                var mongoCompany = new MongoDbCompany(company);
                m_companiesDataSource.SaveCompany(mongoCompany);
            }
        }

        public void DeleteEntity(int id)
        {
            m_companiesDataSource.DeleteCompany(id);
        }

        public bool EntityExists(int id)
        {
            var result = false;
            if (m_companiesDataSource.GetCompany(id) != null)
            {
                result = true;
            }
            return result;
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

        //private IEnumerable<Event> FilterUserId(EventSearchQuery eventSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (eventSearchQuery.UserId.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.UserId == eventSearchQuery.UserId.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterRowStatus(EventSearchQuery eventSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (eventSearchQuery.RowStatus.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.RowStatus == eventSearchQuery.RowStatus.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterTaskId(EventSearchQuery eventSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (eventSearchQuery.TaskId.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.TaskId == eventSearchQuery.TaskId.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterTime(EventSearchQuery eventSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (eventSearchQuery.LastModified.HasValue)
        //    {
        //        contacts = contacts.Where(item => (item.Time >= eventSearchQuery.LastModified.Value));
        //    }
        //    return tasks;
        //}
    }
}
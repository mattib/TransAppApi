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
    public class AuthenticationManager : IEntityManager<AuthenticationUser>
    {
        private MongoDbUsersDataSource m_usersDataSource;

        public AuthenticationManager()
        {
            m_usersDataSource = new MongoDbUsersDataSource();
        }

        public AuthenticationUser[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            throw new NotImplementedException();
        }

        public AuthenticationUser GetEntity(int id)
        {
            var mongoDbUser = m_usersDataSource.GetUser(id);

            var result = new AuthenticationUser(mongoDbUser);

            return result;
        }

        public void SaveEntity(AuthenticationUser[] entities)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        //public void SaveEntity(AuthenticationUser[] tasks)
        //{
        //    foreach (var address in tasks)
        //    {
        //        m_usersDataSource.SaveAddress(address);
        //    }
        //}

        //public void DeleteEntity(int id)
        //{
        //    m_usersDataSource.DeleteAddress(id);
        //}

        public bool EntityExists(int id)
        {
            var result = m_usersDataSource.GetUser(id) != null;
            return result;
        }

        //private IEnumerable<AuthenticationUser> QueryEvents(EntitiesSearchQuery entitiesSearchQuery)
        //{
        //    var addressesList = new List<Address>();

        //    IEnumerable<AuthenticationUser> addressesDataSource = m_usersDataSource.GetAll();

        //    //eventsDataSource = FilterUserId(entitiesSearchQuery, eventsDataSource);

        //    //eventsDataSource = FilterRowStatus(entitiesSearchQuery, eventsDataSource);

        //    //eventsDataSource = FilterTaskId(entitiesSearchQuery, eventsDataSource);

        //    //eventsDataSource = FilterTime(entitiesSearchQuery, eventsDataSource);
        //    return addressesDataSource;
        //}

        //private IEnumerable<Event> FilterUserId(EventSearchQuery entitiesSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (entitiesSearchQuery.UserId.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.UserId == entitiesSearchQuery.UserId.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterRowStatus(EventSearchQuery entitiesSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (entitiesSearchQuery.RowStatus.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.RowStatus == entitiesSearchQuery.RowStatus.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterTaskId(EventSearchQuery entitiesSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (entitiesSearchQuery.TaskId.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.TaskId == entitiesSearchQuery.TaskId.Value));
        //    }
        //    return tasks;
        //}

        //private IEnumerable<Event> FilterTime(EventSearchQuery entitiesSearchQuery, IEnumerable<Company> tasks)
        //{
        //    if (entitiesSearchQuery.LastModified.HasValue)
        //    {
        //        tasks = tasks.Where(item => (item.Time >= entitiesSearchQuery.LastModified.Value));
        //    }
        //    return tasks;
        //}
    }
}
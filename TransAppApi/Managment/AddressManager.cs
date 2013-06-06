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
    public class AddressManager : IEntityManager<Address>
    {
        private MongoDbAddressesDataSource m_addressesDataSource;

        public AddressManager()
        {
            m_addressesDataSource = new MongoDbAddressesDataSource();
        }

        public Address[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<Address>();

            var addressesList = QueryEvents(searchQuery);

            foreach (var mongoDbAddress in addressesList)
            {
                var address = new Address(mongoDbAddress);
                result.Add(address);
            }

            return result.ToArray();
        }

        public Address GetEntity(int id)
        {
            var mongoAddress = m_addressesDataSource.GetAddress(id);

            var result = new Address(mongoAddress);

            return result;
        }

        public void SaveEntity(Address[] tasks)
        {
            foreach (var address in tasks)
            {
                var mongoDbAddress = new MongoDbAddress(address);
                m_addressesDataSource.SaveAddress(mongoDbAddress);
            }
        }

        public void DeleteEntity(int id)
        {
            m_addressesDataSource.DeleteAddress(id);
        }

        private IEnumerable<Address> QueryEvents(EntitiesSearchQuery entitiesSearchQuery)
        {
            var addressesList = new List<Address>();

            IEnumerable<Address> addressesDataSource = m_addressesDataSource.GetAll();

            //eventsDataSource = FilterUserId(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterRowStatus(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTaskId(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTime(entitiesSearchQuery, eventsDataSource);
            return addressesDataSource;
        }

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
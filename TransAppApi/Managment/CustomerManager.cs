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
    public class CustomerManager : IEntityManager<Customer>
    {
        private MongoDbCustomersDataSource m_customersDataSource;

        public CustomerManager()
        {
            m_customersDataSource = new MongoDbCustomersDataSource();
        }

        public Customer[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<Customer>();

            var customersList = QueryEvents(searchQuery);

            foreach (var mongoDbCustomer in customersList)
            {
                var customer = new Customer(mongoDbCustomer);
                result.Add(customer);
            }

            return result.ToArray();
        }

        public Customer GetEntity(int id)
        {
            var mongoCustomer = m_customersDataSource.GetCustomer(id);

            var result = new Customer(mongoCustomer);

            return result;
        }

        public void SaveEntity(Customer[] customers)
        {
            foreach (var customer in customers)
            {
                var mongoDbCustomer = new MongoDbCustomer(customer);
                m_customersDataSource.SaveCustomer(mongoDbCustomer);
            }
        }

        public void DeleteEntity(int id)
        {
            m_customersDataSource.DeleteCustomer(id);
        }

        private IEnumerable<Customer> QueryEvents(EntitiesSearchQuery entitiesSearchQuery)
        {
            var addressesList = new List<Customer>();

            IEnumerable<Customer> customerDataSource = m_customersDataSource.GetAll();

            //eventsDataSource = FilterUserId(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterRowStatus(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTaskId(entitiesSearchQuery, eventsDataSource);

            //eventsDataSource = FilterTime(entitiesSearchQuery, eventsDataSource);
            return customerDataSource;
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
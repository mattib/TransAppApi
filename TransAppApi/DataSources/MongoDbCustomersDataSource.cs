using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public class MongoDbCustomersDataSource : MongoDbDataSource, ICustomersDataSource
    {
        private readonly string m_customersDataBaseName = "customers";

        private MongoCollection<MongoDbCustomer> GetCustomersCollection()
        {
            var collections = DbManager.GetCollection<MongoDbCustomer>(m_customersDataBaseName);

            return collections;
        }

        public Customer[] GetAll()
        {
            var customersCollection = GetCustomersCollection();
            var query = Query<MongoDbCustomer>.Where(e => e.RowStatus != 1);
            var customers = customersCollection.Find(query);

            return customers.ToArray();
        }

        public Customer GetCustomer(int id)
        {
            var customersCollection = GetCustomersCollection();
            var query = Query<MongoDbCustomer>.EQ(e => e.Id, id);
            var customer = customersCollection.FindOne(query);

            return customer;
        }

        //public Company GetUser(string userName)
        //{
        //    var comapniesCollection = GetContactsCollection();
        //    var query = Query<MongoDbCompany>.EQ(e => e.UserName, userName);
        //    var contact = comapniesCollection.FindOne(query);

        //    return contact;
        //}

        public void SaveCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                customer.Id = NewId();
            }

            var mongoDbCustomer = new MongoDbCustomer(customer);

            var customersCollection = GetCustomersCollection();
            customersCollection.Save(mongoDbCustomer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            customer.RowStatus = 1;
            var customersCollection = GetCustomersCollection();
            customersCollection.Save(customer);
        }

        public int NewId()
        {
            var customers = GetAll();
            var result = 0;
            foreach (var customer in customers)
            {
                var mongoDbCustomer = (MongoDbCustomer)customer;
                if (mongoDbCustomer.MongoId.Pid > result)
                {
                    result = mongoDbCustomer.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
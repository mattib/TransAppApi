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

            return ToCustomersArray(customers);
        }

        public Customer GetCustomer(int id)
        {
            var customersCollection = GetCustomersCollection();
            var query = Query<MongoDbCustomer>.EQ(e => e.Id, id);
            var customer = customersCollection.FindOne(query);

            return ToCustomer(customer);
        }

        public void SaveCustomer(Customer customer)
        {
            if (customer.Id == 0)
            {
                customer.Id = NewId();
                var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();
                var addrssId = mongoDbAddressesDataSource.SaveAddress(customer.Address);
                customer.Address.Id = addrssId;
                var mongoDbContactsDataSource = new MongoDbContactsDataSource();
                var contactId = mongoDbContactsDataSource.SaveContact(customer.Contact);
                customer.Contact.Id = addrssId;
                customer.Contact.Address = new Address();
                customer.Contact.Address.Id = addrssId;
                
            }

            var mongoDbCustomer = new MongoDbCustomer(customer);
            mongoDbCustomer.LastModified = DateTime.UtcNow;
            var customersCollection = GetCustomersCollection();
            customersCollection.Save(mongoDbCustomer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            customer.RowStatus = 1;
            customer.LastModified = DateTime.UtcNow;
            var customersCollection = GetCustomersCollection();
            customersCollection.Save(customer);
        }

        public int NewId()
        {
            var customers = GetAll();
            var result = 0;
            foreach (var customer in customers)
            {
                var mongoDbCustomer = new MongoDbCustomer(customer);
                if (mongoDbCustomer.MongoId.Pid > result)
                {
                    result = mongoDbCustomer.MongoId.Pid;
                }
            }

            return result + 1;
        }

        private Customer[] ToCustomersArray(IEnumerable<MongoDbCustomer> mongoDbCustomers)
        {
            var result = mongoDbCustomers.Select(ToCustomer);
            return result.ToArray();
        }

        private Customer ToCustomer(MongoDbCustomer mongoDbCustomer)
        {
            var customer = new Customer();
            customer.Id = mongoDbCustomer.Id;
            customer.Name = mongoDbCustomer.Name;

            var address = GetAddress(mongoDbCustomer);
            customer.Address = new Address(address);

            var contact = GetContact(mongoDbCustomer);
            customer.Contact = new Contact(contact);

            var company = GetCompany(mongoDbCustomer);
            customer.Company = new Company(company);

            customer.LastModified = mongoDbCustomer.LastModified;
            customer.RowStatus = mongoDbCustomer.RowStatus;

            return customer;
        }

        private static Address GetAddress(MongoDbCustomer mongoDbCustomer)
        {
            var result = default(Address);
            var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();

            result = mongoDbAddressesDataSource.GetAddress(mongoDbCustomer.AddressId);
            return result;
        }

        private static Contact GetContact(MongoDbCustomer mongoDbCustomer)
        {
            var result = default(Contact);
            var mongoDbContactsDataSource = new MongoDbContactsDataSource();

            result = mongoDbContactsDataSource.GetContact(mongoDbCustomer.ContactId);
            return result;
        }

        private static Company GetCompany(MongoDbCustomer mongoDbCustomer)
        {
            var result = default(Company);
            var mongoDbCompaniesDataSource = new MongoDbCompaniesDataSource();

            result = mongoDbCompaniesDataSource.GetCompany(mongoDbCustomer.CompanyId);
            return result;
        }
    }
}
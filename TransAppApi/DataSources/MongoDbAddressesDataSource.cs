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
    public class MongoDbAddressesDataSource : MongoDbDataSource, IAddressesDataSource
    {
        private readonly string m_addressesDataBaseName = "addresss";

        private MongoCollection<MongoDbAddress> GetAddressesCollection()
        {
            var collections = DbManager.GetCollection<MongoDbAddress>(m_addressesDataBaseName);

            return collections;
        }

        public Address[] GetAll()
        {
            var addressesCollection = GetAddressesCollection();
            var query = Query<MongoDbAddress>.Where(e => e.RowStatus != 1);
            var addresses = addressesCollection.Find(query);

            return addresses.ToArray();
        }

        public Address GetAddress(int id)
        {
            var addressesCollection = GetAddressesCollection();
            var query = Query<MongoDbAddress>.EQ(e => e.Id, id);
            var address = addressesCollection.FindOne(query);

            return address;
        }

        //public Company GetUser(string userName)
        //{
        //    var comapniesCollection = GetContactsCollection();
        //    var query = Query<MongoDbCompany>.EQ(e => e.UserName, userName);
        //    var contact = comapniesCollection.FindOne(query);

        //    return contact;
        //}

        public void SaveAddress(Address address)
        {
            if (address.Id == 0)
            {
                address.Id = NewId();
            }

            var mongoDbAddress = new MongoDbAddress(address);

            var addressesCollection = GetAddressesCollection();
            addressesCollection.Save(mongoDbAddress);
        }

        public void DeleteAddress(int id)
        {
            var address = GetAddress(id);
            address.RowStatus = 1;
            var addressesCollection = GetAddressesCollection();
            addressesCollection.Save(address);
        }

        public int NewId()
        {
            var adressed = GetAll();
            var result = 0;
            foreach (var adress in adressed)
            {
                var mongoDbAddress = (MongoDbAddress)adress;
                if (mongoDbAddress.MongoId.Pid > result)
                {
                    result = mongoDbAddress.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
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

            return ToAddressesArray(addresses);
        }

        public Address GetAddress(int id)
        {
            var addressesCollection = GetAddressesCollection();
            var query = Query<MongoDbAddress>.EQ(e => e.Id, id);
            var address = addressesCollection.FindOne(query);

            return ToAddress(address);
        }

        public int SaveAddress(Address address)
        {
            if (address.Id == 0)
            {
                address.Id = NewId();
            }

            var mongoDbAddress = new MongoDbAddress(address) {LastModified = DateTime.UtcNow};
            mongoDbAddress.LastModified = DateTime.UtcNow;

            var addressesCollection = GetAddressesCollection();
            addressesCollection.Save(mongoDbAddress);
            return address.Id;
        }

        public void DeleteAddress(int id)
        {
            var address = GetAddress(id);
            address.RowStatus = 1;
            address.LastModified = DateTime.UtcNow;
            var addressesCollection = GetAddressesCollection();
            addressesCollection.Save(address);
        }

        public int NewId()
        {
            var addressesCollection = GetAddressesCollection();
            var amountOfAddresses = (int)addressesCollection.Count();

            return amountOfAddresses + 1;
        }

        private Address[] ToAddressesArray(IEnumerable<MongoDbAddress> mongoDbAddresses)
        {
            var result =  mongoDbAddresses.Select(ToAddress);
            return result.ToArray();
        }

        private Address ToAddress(MongoDbAddress mongoDbAddress)
        {
            var address = new Address();
            address.Id = mongoDbAddress.Id;
            address.StreetName = mongoDbAddress.StreetName;
            address.StreetNumber = mongoDbAddress.StreetNumber;
            address.City = mongoDbAddress.City;
            address.District = mongoDbAddress.District;
            address. Country = mongoDbAddress.Country;
            address.PostalCode = mongoDbAddress.PostalCode;
            address.LastModified = mongoDbAddress.LastModified;
            address.RowStatus = mongoDbAddress.RowStatus;
            return address;
        }
    }
}
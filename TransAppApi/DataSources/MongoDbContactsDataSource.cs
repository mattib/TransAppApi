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
    public class MongoDbContactsDataSource : MongoDbDataSource, IContactsDataSource
    {
        private readonly string m_conactsDataBaseName = "contacts";

        private MongoCollection<MongoDbContact> GetContactsCollection()
        {
            var collections = DbManager.GetCollection<MongoDbContact>(m_conactsDataBaseName);

            return collections;
        }

        public Contact[] GetAll()
        {
            var contactsCollection = GetContactsCollection();
            var query = Query<MongoDbContact>.Where(e => e.RowStatus != 1);
            var contacts = contactsCollection.Find(query);

            return ToContactsArray(contacts);
        }

        public Contact GetContact(int id)
        {
            var contactsCollection = GetContactsCollection();
            var query = Query<MongoDbContact>.EQ(e => e.Id, id);
            var contact = contactsCollection.FindOne(query);

            return ToContact(contact);
        }

        public int SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                contact.Id = NewId();
                var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();
                var addrssId = mongoDbAddressesDataSource.SaveAddress(contact.Address);
                contact.Address.Id = addrssId;
            }

            var MongoDbUser = new MongoDbContact(contact);

            var contactsCollection = GetContactsCollection();
            contactsCollection.Save(MongoDbUser);
            return contact.Id;
        }

        public void DeleteContact(int id)
        {
            var contact = GetContact(id);
            contact.RowStatus = 1;
            var contactsCollection = GetContactsCollection();
            contactsCollection.Save(contact);
        }

        public int NewId()
        {
            var contacts = GetAll();
            var result = 0;
            foreach (var contact in contacts)
            {
                var mongoDbContact = new MongoDbContact(contact);
                if (mongoDbContact.MongoId.Pid > result)
                {
                    result = mongoDbContact.MongoId.Pid;
                }
            }

            return result + 1;
        }

        private Contact[] ToContactsArray(IEnumerable<MongoDbContact> mongoDbContacts)
        {
            var result = mongoDbContacts.Select(ToContact);
            return result.ToArray();
        }

        private Contact ToContact(MongoDbContact mongoDbContact)
        {
            var contact = new Contact();
            contact.Id = mongoDbContact.Id;
            contact.FirstName = mongoDbContact.FirstName;
            contact.LastName = mongoDbContact.LastName;
            contact.OfficeNumber = mongoDbContact.OfficeNumber;
            contact.CellNumber = mongoDbContact.CellNumber;
            contact.Email = mongoDbContact.Email;

            var address = GetAddress(mongoDbContact);
            contact.Address = new Address(address);

            contact.LastModified = mongoDbContact.LastModified;
            contact.RowStatus = mongoDbContact.RowStatus;

            return contact;
        }

        private static Address GetAddress(MongoDbContact mongoDbContact)
        {
            var result = default(Address);
            var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();

            result = mongoDbAddressesDataSource.GetAddress(mongoDbContact.AddressId);
            return result;
        }
    }
}
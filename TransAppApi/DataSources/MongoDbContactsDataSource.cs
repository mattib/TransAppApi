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

            return contacts.ToArray();
        }

        public Contact GetContact(int id)
        {
            var contactsCollection = GetContactsCollection();
            var query = Query<MongoDbContact>.EQ(e => e.Id, id);
            var contact = contactsCollection.FindOne(query);

            return contact;
        }

        //public Company GetUser(string userName)
        //{
        //    var comapniesCollection = GetContactsCollection();
        //    var query = Query<MongoDbCompany>.EQ(e => e.UserName, userName);
        //    var contact = comapniesCollection.FindOne(query);

        //    return contact;
        //}

        public void SaveContact(Contact contact)
        {
            if (contact.Id == 0)
            {
                contact.Id = NewId();
            }

            var MongoDbUser = new MongoDbContact(contact);

            var contactsCollection = GetContactsCollection();
            contactsCollection.Save(MongoDbUser);
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
                var mongoDbContact = (MongoDbContact)contact;
                if (mongoDbContact.MongoId.Pid > result)
                {
                    result = mongoDbContact.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
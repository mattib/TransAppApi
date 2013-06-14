using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbContact
    {
        private ObjectId m_mongoId;

        public MongoDbContact()
        {
        }

        public MongoDbContact(Contact contact)
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(contact.Id.ToString()), 0);
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            OfficeNumber = contact.OfficeNumber;
            CellNumber = contact.CellNumber;
            Email = contact.Email;
            AddressId = contact.Address.Id;
            LastModified = contact.LastModified;
            RowStatus = contact.RowStatus;
            CompanyId = contact.Company.Id;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeNumber { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public int CompanyId { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public ObjectId MongoId
        {
            get { return m_mongoId; }
            set
            {
                m_mongoId = value;
                Id = (int)value.Pid;
            }
        }
        
    }
}
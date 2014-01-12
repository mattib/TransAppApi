using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbCustomer
    {
        private ObjectId m_mongoId;

        public MongoDbCustomer()
        {
        }

        public MongoDbCustomer(Customer customer)
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(customer.Id.ToString()), 0);
            Name = customer.Name;
            AddressId = customer.Address.Id;
            ContactId = customer.Contact.Id;
            CompanyId = customer.Company.Id;
            LastModified = customer.LastModified;
            RowStatus = customer.RowStatus;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public int ContactId { get; set; }
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
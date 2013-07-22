using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbAddress 
    {
        private ObjectId m_mongoId;

        public MongoDbAddress()
        {
        }

        public MongoDbAddress(Address address)
        {
            MongoId = new ObjectId(DateTime.Now, 0, short.Parse(address.Id.ToString()), 0);
            StreetName = address.StreetName;
            StreetNumber = address.StreetNumber;
            City = address.City;
            District = address.District;
            Country = address.Country;
            PostalCode = address.PostalCode;
            LastModified = address.LastModified;
            RowStatus = address.RowStatus;
        }


        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
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
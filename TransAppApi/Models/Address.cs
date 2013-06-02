using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Models
{
    public class Address
    {
        public Address()
        { }

        public Address(Address address)
        {
            Id = address.Id;
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
    }
}
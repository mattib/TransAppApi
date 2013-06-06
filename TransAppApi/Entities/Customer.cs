using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Customer
    {
        public Customer()
        { }

        public Customer(Customer custumer)
        {
            Id = custumer.Id;
            Name = custumer.Name;
            AddressId = custumer.AddressId;
            ContactId = custumer.ContactId;
            LastModified = custumer.LastModified;
            RowStatus = custumer.RowStatus;
            CompanyId = custumer.CompanyId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public int ContactId { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int CompanyId { get; set; }
    }
}
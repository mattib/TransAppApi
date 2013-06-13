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

        public Customer(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Address = new Address(customer.Address);
            Contact = new Contact(customer.Contact);
            Company = new Company(customer.Company);
            LastModified = customer.LastModified;
            RowStatus = customer.RowStatus;
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Company Company { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        
    }
}
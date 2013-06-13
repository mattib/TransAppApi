using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Contact
    {
        public Contact()
        { }

        public Contact(Contact contact)
        {
            Id = contact.Id;
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            OfficeNumber = contact.OfficeNumber;
            CellNumber = contact.CellNumber;
            Email = contact.Email;
            Address = new Address(contact.Address);
            LastModified = contact.LastModified;
            RowStatus = contact.RowStatus;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeNumber { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
    }

     
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class User
    {
        public User()
        { }

        public User(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            ReferenceId = user.ReferenceId;
            Password = user.Password;
            Company = user.Company;
            Role = user.Role;
            TimeCreated = user.TimeCreated;
            LastModified = user.LastModified;
            RowStatus = user.RowStatus;
            Active = user.Active;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ReferenceId { get; set; }
        public string Password { get; set; }
        public Company Company { get; set; }
        public int? Role { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int? Active { get; set; }
    }
}
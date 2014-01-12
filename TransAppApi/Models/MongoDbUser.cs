using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbUser
    {
        private ObjectId m_mongoId;

        public MongoDbUser()
        {
        }

        public MongoDbUser(User user)
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(user.Id.ToString()), 0);
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            ReferenceId = user.ReferenceId;
            Password = user.Password;
            CompanyId = user.Company.Id;
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
        public int CompanyId { get; set; }
        public int? Role { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int? Active { get; set; }
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
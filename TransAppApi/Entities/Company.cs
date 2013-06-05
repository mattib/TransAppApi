using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Company
    {
        public Company()
        { }

        public Company(Company company)
        {
            Id = company.Id;
            Name = company.Name;
            CompanyUserName = company.CompanyUserName;
            ContactUserId = company.ContactUserId;
            AddressId = company.AddressId;
            Created = company.Created;
            LastModified = company.LastModified;
            RowStatus = company.RowStatus;
            AmountOfUsers = company.AmountOfUsers;
            AmountOfTasksPerUser = company.AmountOfTasksPerUser;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyUserName { get; set; }
        public int ContactUserId { get; set; }
        public int AddressId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int Active { get; set; }
        public int AmountOfUsers { get; set; }
        public int AmountOfTasksPerUser { get; set; }
    }
}
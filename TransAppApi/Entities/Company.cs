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
            Address = new Address(company.Address);
            Created = company.Created;
            LastModified = company.LastModified;
            RowStatus = company.RowStatus;
            AmountOfUsers = company.AmountOfUsers;
            AmountOfTasksPerUser = company.AmountOfTasksPerUser;
            CompanyInfo = company.CompanyInfo;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyUserName { get; set; }
        public Address Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int? AmountOfUsers { get; set; }
        public int? AmountOfTasksPerUser { get; set; }
        public string CompanyInfo { get; set; }

    }
}
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbCompany
    {
        private ObjectId m_mongoId;

        public MongoDbCompany()
        {
        }

        public MongoDbCompany(Company company)
        {
            MongoId = new ObjectId(DateTime.Now, 0, short.Parse(company.Id.ToString()), 0);
            Name = company.Name;
            CompanyUserName = company.CompanyUserName;
            AddressId = company.Address.Id;
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
        public int AddressId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int RowStatus { get; set; }
        public int? AmountOfUsers { get; set; }
        public int? AmountOfTasksPerUser { get; set; }
        public string CompanyInfo { get; set; }
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
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public class MongoDbCompaniesDataSource : MongoDbDataSource, ICompaniesDataSource
    {
        private readonly string m_companiesDataBaseName = "companies";

        private MongoCollection<MongoDbCompany> GetCompaniesCollection()
        {
            var collections = DbManager.GetCollection<MongoDbCompany>(m_companiesDataBaseName);

            return collections;
        }

        public Company[] GetAll()
        {
            var comapniesCollection = GetCompaniesCollection();
            var query = Query<MongoDbCompany>.Where(e => e.RowStatus != 1);
            var companies = comapniesCollection.Find(query);

            return ToCompaniesArray(companies);
        }

        public Company GetCompany(int id)
        {
            var comapniesCollection = GetCompaniesCollection();
            var query = Query<MongoDbCompany>.EQ(e => e.Id, id);
            var company = comapniesCollection.FindOne(query);

            return ToCompany(company);
        }

        public void SaveCompany(Company comapny)
        {
            if (comapny.Id == 0)
            {
                comapny.Id = NewId();

                var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();
                var addrssId = mongoDbAddressesDataSource.SaveAddress(comapny.Address);
                comapny.Address.Id = addrssId;
                
            }

            var mongoDbCompany = new MongoDbCompany(comapny);
            mongoDbCompany.LastModified = DateTime.Now;

            var comapniesCollection = GetCompaniesCollection();
            comapniesCollection.Save(mongoDbCompany);
        }

        public void DeleteCompany(int id)
        {
            var comapny = GetCompany(id);
            comapny.RowStatus = 1;
            comapny.LastModified = DateTime.Now;
            var comapniesCollection = GetCompaniesCollection();
            comapniesCollection.Save(comapny);
        }

        public int NewId()
        {
            var comapniesCollection = GetCompaniesCollection();
            var amountOfCompanies = (int)comapniesCollection.Count();

            return amountOfCompanies + 1;
        }

        private Company[] ToCompaniesArray(IEnumerable<MongoDbCompany> mongoDbCompanies)
        {
            var result = mongoDbCompanies.Select(ToCompany);
            return result.ToArray();
        }

        private Company ToCompany(MongoDbCompany mongoDbCompany)
        {

            var company = new Company();
            company.Id = mongoDbCompany.Id;
            company.Name = mongoDbCompany.Name;
            company.CompanyUserName = mongoDbCompany.CompanyUserName;


            var address = GetAddress(mongoDbCompany);
            company.Address = new Address(address);
            company.Created = mongoDbCompany.Created;
            company.LastModified = mongoDbCompany.LastModified;
            company.RowStatus = mongoDbCompany.RowStatus;
            company.AmountOfUsers = mongoDbCompany.AmountOfUsers;
            company.AmountOfTasksPerUser = mongoDbCompany.AmountOfTasksPerUser;
            company.CompanyInfo = mongoDbCompany.CompanyInfo;

            return company;
        }

        private static Address GetAddress(MongoDbCompany mongoDbCompany)
        {
            var result = default(Address);
            var mongoDbAddressesDataSource = new MongoDbAddressesDataSource();

            result = mongoDbAddressesDataSource.GetAddress(mongoDbCompany.AddressId);
            return result;
        }
    }
}
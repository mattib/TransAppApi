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
            var comapnies = comapniesCollection.Find(query);

            return comapnies.ToArray();
        }

        public Company GetCompany(int id)
        {
            var comapniesCollection = GetCompaniesCollection();
            var query = Query<MongoDbCompany>.EQ(e => e.Id, id);
            var comapny = comapniesCollection.FindOne(query);

            return comapny;
        }

        //public Company GetUser(string userName)
        //{
        //    var comapniesCollection = GetCompaniesCollection();
        //    var query = Query<MongoDbCompany>.EQ(e => e.UserName, userName);
        //    var comapny = comapniesCollection.FindOne(query);

        //    return comapny;
        //}

        public void SaveCompany(Company comapny)
        {
            if (comapny.Id == 0)
            {
                comapny.Id = NewId();
            }

            var mongoDbCompany = new MongoDbCompany(comapny);

            var comapniesCollection = GetCompaniesCollection();
            comapniesCollection.Save(mongoDbCompany);
        }

        public void DeleteCompany(int id)
        {
            var comapny = GetCompany(id);
            comapny.RowStatus = 1;
            var comapniesCollection = GetCompaniesCollection();
            comapniesCollection.Save(comapny);
        }

        public int NewId()
        {
            var companies = GetAll();
            var result = 0;
            foreach (var item in companies)
            {
                var mongoDbCompany = (MongoDbCompany)item;
                if (mongoDbCompany.MongoId.Pid > result)
                {
                    result = mongoDbCompany.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
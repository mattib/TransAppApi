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
        public class MongoDbUsersDataSource : MongoDbDataSource, IUsersDataSource
    {
        private readonly string m_usersDataBaseName = "users";

        private MongoCollection<MongoDbUser> GetUsersCollection()
        {
            var collections = DbManager.GetCollection<MongoDbUser>(m_usersDataBaseName);

            return collections;
        }

        public User[] GetAll()
        {
            var UsersCollection = GetUsersCollection();
            var query = Query<MongoDbUser>.Where(e => e.RowStatus != 1);
            var Users = UsersCollection.Find(query);

            return ToUsersArray(Users);
        }

        public User GetUser(int id)
        {
            var UsersCollection = GetUsersCollection();
            var query = Query<MongoDbUser>.EQ(e => e.Id, id);
            var user = UsersCollection.FindOne(query);

            return ToUser(user);
        }

        public User GetUser(string userName)
        {
            var UsersCollection = GetUsersCollection();
            var query = Query<MongoDbUser>.EQ(e => e.UserName, userName);
            var user = UsersCollection.FindOne(query);
            if (user == null)
                return null;
            return ToUser(user);
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                user.Id = NewId();
                user.TimeCreated = DateTime.Now;
            }
            else
            {
               var savedUser = GetUser(user.Id);
               user.Password = savedUser.Password;
               user.Company = savedUser.Company;
               user.RowStatus = savedUser.RowStatus;
               user.Active = savedUser.Active;
            }

            var mongoDbUser = new MongoDbUser(user);
            mongoDbUser.LastModified = DateTime.Now;
            var UsersCollection = GetUsersCollection();
            UsersCollection.Save(mongoDbUser);
        }

        public void DeleteUser(int id)
        {
            var UserItem = GetUser(id);
            UserItem.RowStatus = 1;
            UserItem.LastModified = DateTime.Now;
            var UsersCollection = GetUsersCollection();
            UsersCollection.Save(UserItem);
        }

        public int NewId()
        {
            var UsersCollection = GetUsersCollection();
            var amountOfUsers = (int)UsersCollection.Count();

            return amountOfUsers + 1;
        }

        private User[] ToUsersArray(IEnumerable<MongoDbUser> mongoDbUsers)
        {
            var result = mongoDbUsers.Select(ToUser);
            return result.ToArray();
        }

        private User ToUser(MongoDbUser mongoDbUser)
        {
            var user = new User();
            user.Id = mongoDbUser.Id;
            user.FirstName = mongoDbUser.FirstName;
            user.LastName = mongoDbUser.LastName;
            user.UserName = mongoDbUser.UserName;
            user.PhoneNumber = mongoDbUser.PhoneNumber  ;
            user.Email = mongoDbUser.Email;
            user.ReferenceId = mongoDbUser.ReferenceId;
            user.Password = mongoDbUser.Password;

            var company = GetCompany(mongoDbUser);
            user.Company = new Company(company);

            user.Role = mongoDbUser.Role;
            user.TimeCreated = mongoDbUser.TimeCreated;
            user.LastModified = mongoDbUser.LastModified;
            user.RowStatus = mongoDbUser.RowStatus;
            user.Active = mongoDbUser.Active;
            
            return user;
        }

        private static Company GetCompany(MongoDbUser mongoDbUser)
        {
            var result = default(Company);
            var mongoDbCompaniesDataSource = new MongoDbCompaniesDataSource();

            result = mongoDbCompaniesDataSource.GetCompany(mongoDbUser.CompanyId);
            return result;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var result = false;
            var savedUser = GetUser(userId);

            if (savedUser.Password == oldPassword)
            {
                savedUser.Password = newPassword;
                var mongoDbUser = new MongoDbUser(savedUser);
                mongoDbUser.LastModified = DateTime.Now;
                var UsersCollection = GetUsersCollection();
                UsersCollection.Save(mongoDbUser);
                result = true;
            }
            else
            {
                throw new ArgumentException("Invalid Password");
            }
            return result;
        }

        public int AuthenticateUser(string userName, string password)
        {
            var result = -1;

            var savedUser = GetUser(userName);
            if (savedUser != null && savedUser.Password == password)
            {
                result = savedUser.Id;
            }

            return result;
        }

        public int CreateUser(string userName, string password, int companyId)
        {
            // if companyId exists
            var user = new User();
            user.Company = new Company();
            user.Company.Id = companyId;
            user.Id = 0;
            user.UserName = userName;
            user.Password = password;

            SaveUser(user);

            var newUser = GetUser(userName);

            return newUser.Id;
        }
    }
}
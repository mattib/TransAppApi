using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.DataSources;
using TransAppApi.Entities;
using TransAppApi.Models;
using TransAppApi.SearchQueries;

namespace TransAppApi.Managment
{
    public class UserManager : IUserManager
    {
        private MongoDbUsersDataSource m_userDataSource;

        public UserManager()
        {
            m_userDataSource = new MongoDbUsersDataSource();
        }

        public User GetUser(string userName)
        {
            var mongoUser = m_userDataSource.GetUser(userName);

            var result = new User(mongoUser);

            result.Password = null;

            return result;
        }

        public User[] GetEntities(EntitiesSearchQuery searchQuery)
        {
            var result = new List<User>();

            var usersList = QueryUsers(searchQuery);

            foreach (var mongoUser in usersList)
            {
                var user = new User(mongoUser);
                user.Password = null;
                result.Add(user);
            }

            return result.ToArray();
        }

        public User GetEntity(int id)
        {
            var mongoUser = m_userDataSource.GetUser(id);

            var result = new User(mongoUser);

            return result;
        }

        public void SaveEntity(User[] users)
        {
            foreach (var user in users)
            {
                m_userDataSource.SaveUser(user);
            }
        }

        public void DeleteEntity(int id)
        {
            m_userDataSource.DeleteUser(id);
        }

        public bool EntityExists(int id)
        {
            var result = m_userDataSource.GetUser(id) != null;
            return result;
        }

        private IEnumerable<User> QueryUsers(EntitiesSearchQuery entitiesSearchQuery)
        {
            var tasksList = new List<Task>();

            var usersSearchQuery = entitiesSearchQuery as UsersSearchQuery;

            IEnumerable<User> usersDataSource = m_userDataSource.GetAll();

            usersDataSource = FilterCompanyId(usersSearchQuery, usersDataSource);

            usersDataSource = FilterRowStatus(usersSearchQuery, usersDataSource);
            return usersDataSource;
        }

        private IEnumerable<User> FilterRowStatus(UsersSearchQuery usersSearchQuery, IEnumerable<User> users)
        {
            if (usersSearchQuery.RowStatus.HasValue)
            {
                users = users.Where(user => (user.RowStatus == usersSearchQuery.RowStatus.Value));
            }
            return users;
        }

        private IEnumerable<User> FilterCompanyId(UsersSearchQuery usersSearchQuery, IEnumerable<User> users)
        {
            if (usersSearchQuery.CompanyId.HasValue)
            {
                users = users.Where(user => (user.Company.Id == usersSearchQuery.CompanyId.Value));
            }
            return users;
        }

        public bool AuthenticateUser(string userName, string password)
        {
            var result = m_userDataSource.AuthenticateUser(userName, password);
            return result;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var result = m_userDataSource.ChangePassword(userId, oldPassword, newPassword);
            return result;
        }

        public int CreateUser(string userName, string password, int companyId)
        {
            var result = m_userDataSource.CreateUser(userName, password, companyId);
            return result;
        }

    }
}
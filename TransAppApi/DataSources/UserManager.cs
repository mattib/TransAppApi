using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public class UserManager : IUserManager
    {
        private MongoDbUsersDataSource m_userDataSource;

        public UserManager()
        {
            m_userDataSource = new MongoDbUsersDataSource();
        }

        public User[] GetUsers()
        {
            var result = new List<User>();

            var usersList = QueryUsers();

            foreach (var mongoUser in usersList)
            {
                var user = new User(mongoUser);
                result.Add(user);
            }

            return result.ToArray();
        }

        public User GetUser(int id)
        {
            var mongoUser = m_userDataSource.GetUser(id);

            var result = new User(mongoUser);

            return result;
        }

        public User GetUser(string userName)
        {
            var mongoUser = m_userDataSource.GetUser(userName);

            var result = new User(mongoUser);

            return result;
        }

        public void SaveUser(User user)
        {
            var mongoUser = new MongoDbUser(user);
            m_userDataSource.SaveUser(mongoUser);
        }

        public void DeleteUser(int id)
        {
            m_userDataSource.DeleteUser(id);
        }

        private IEnumerable<User> QueryUsers()
        {
            var usersList = new List<Event>();

            IEnumerable<User> usersDataSource = m_userDataSource.GetAll();
            
            usersDataSource = FilterRowStatus(usersDataSource);

            return usersDataSource;
        }

        private IEnumerable<User> FilterRowStatus(IEnumerable<User> users)
        {
            users = users.Where(item => (item.RowStatus == 0));
            return users;
        }
    }
}
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            return Users.ToArray();
        }

        public User GetUser(int id)
        {
            var UsersCollection = GetUsersCollection();
            var query = Query<MongoDbUser>.EQ(e => e.Id, id);
            var UserItem = UsersCollection.FindOne(query);

            return UserItem;
        }

        public void SaveUser(User UserItem)
        {
            if (UserItem.Id == 0)
            {
                UserItem.Id = NewId();
            }

            var MongoDbUser = new MongoDbUser(UserItem);

            var UsersCollection = GetUsersCollection();
            UsersCollection.Save(MongoDbUser);
        }

        public void DeleteUser(int id)
        {
            var UserItem = GetUser(id);
            UserItem.RowStatus = 1;
            var UsersCollection = GetUsersCollection();
            UsersCollection.Save(UserItem);
        }

        public int NewId()
        {
            var Users = GetAll();
            var result = 0;
            foreach (var item in Users)
            {
                var MongoDbUser = (MongoDbUser)item;
                if (MongoDbUser.MongoId.Pid > result)
                {
                    result = MongoDbUser.MongoId.Pid;
                }
            }

            return result + 1;
        }
    }
}
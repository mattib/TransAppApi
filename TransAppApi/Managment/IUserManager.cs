using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;

namespace TransAppApi.Managment
{
    public interface IUserManager
    {
        User[] GetUsers();

        User GetUser(int id);

        User GetUser(string userName);

        void SaveUser(User user);

        void DeleteUser(int id);
    }
}
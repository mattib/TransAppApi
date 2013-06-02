﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Models;

namespace TransAppApi.DataSources
{
    public interface IUserManager
    {
        User[] GetUsers();

        User GetUser(int id);

        void SaveUser(User user);

        void DeleteUser(int id);
    }
}
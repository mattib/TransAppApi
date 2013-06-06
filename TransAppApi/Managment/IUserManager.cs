using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;
using TransAppApi.Models;

namespace TransAppApi.Managment
{
    public interface IUserManager : IEntityManager<User>
    {
        User GetUser(string userName);
    }
}
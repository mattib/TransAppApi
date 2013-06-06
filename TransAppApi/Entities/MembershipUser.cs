using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class MembershipUser
    {
        public MembershipUser()
        { }

        public MembershipUser(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Password = user.Password;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
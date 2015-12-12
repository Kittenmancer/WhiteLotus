using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate;
using NHibernate.Linq;

namespace WhiteLotus.Models.Queries
{
    public class UserFromEmail : IQuery<Users>
    {
        private readonly string _username;

        public UserFromEmail(string email)
        {
            _username = email;
        }

        public Users Execute(ISession session)
        {
            return session.Query<Users>().FirstOrDefault(u => u.Email == _username);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Linq;
using WhiteLotus.Models.Entities;

namespace WhiteLotus.Models.Queries
{
    public class UserFromEmail : IQuery<Users>
    {
        private readonly string _username;

        public UserFromEmail(string username)
        {
            _username = username;
        }

        public Users Execute(ISession session)
        {
            return session.Query<Users>().FirstOrDefault(u => u.Email == _username);
        }
    }
}
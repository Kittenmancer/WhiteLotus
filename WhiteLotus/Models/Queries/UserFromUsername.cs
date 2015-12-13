using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate;
using NHibernate.Linq;

namespace WhiteLotus.Models.Queries
{
        public class UserFromUsername : IQuery<Users>
        {
            private readonly string _username;

            public UserFromUsername(string username)
            {
                _username = username;
            }

            public Users Execute(ISession session)
            {
                return session.Query<Users>().FirstOrDefault(u => u.Username == _username);
            }
        }
    }

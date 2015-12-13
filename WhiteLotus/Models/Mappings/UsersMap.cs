using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WhiteLotus.Models.Mapping
{
    public class UsersMap:ClassMapping<Users>
    {
        public UsersMap()
        {
            Table("Users");

            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Username);
            Property(x => x.Title);
            Property(x => x.Forename);
            Property(x => x.Surname);
            Property(x => x.Experience);
            Property(x => x.Health);
            Property(x => x.Email);
            Property(x => x.Password);
            Property(x => x.Deleted);
            Property(x => x.isManager);
            Property(x => x.isDeveloper);
        }
    }
}
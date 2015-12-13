using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace WhiteLotus.Models.Mappings
{
    public class ClassesMap:ClassMapping<Classes>
    {
        public ClassesMap()
        {
            Table("Classes");

            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Name);
            Property(x => x.Day);
            Property(x => x.StartTime);
            Property(x => x.Duration);
            Property(x => x.Capacity);
            Property(x => x.TaughtBy);
            Property(x => x.Deleted);
        }
    }
}
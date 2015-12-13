using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace WhiteLotus.Models.Mappings
{
    public class BookingMap:ClassMapping<Booking>
    {
        public BookingMap()
        {
            Table("Booking");

            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.ClassBooked);
            ManyToOne(x => x.BookedBy);
            Property(x => x.GonGiveItToYa);
        }
    }
}
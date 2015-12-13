using System.Collections.Generic;
using WhiteLotus.Models.Entities;
using NHibernate;

namespace WhiteLotus.Models.Queries
{
    public class AllBookings : IMultiQuery<Booking>
    {
        public IEnumerable<Booking> Execute(ISession session)
        {
            return session.CreateQuery("from Booking where Deleted = 0 order by Name").List<Booking>();
        }
    }
}
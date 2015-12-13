using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using WhiteLotus.Models.Mappings;
using WhiteLotus.Models.Queries;
using WhiteLotus.Filters;
using WhiteLotus.Controllers;
using PagedList;

namespace WhiteLotus.ViewModels
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<Classes> Classes { get; set; }
    }
}
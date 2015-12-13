using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;

namespace WhiteLotus.ViewModels
{
    public class BookingViewModel
    {
        public Users User { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public Booking Booking { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
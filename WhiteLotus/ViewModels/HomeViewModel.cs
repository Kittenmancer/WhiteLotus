using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;

namespace WhiteLotus.ViewModels
{
    public class HomeViewModel
    {
        public Users User { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<Booking> Booking { get; set; }
    }
}
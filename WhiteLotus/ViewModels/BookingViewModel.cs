using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using NHibernate;

namespace WhiteLotus.ViewModels
{
    public class BookingViewModel
    {
        public Classes Class { get; set; }
        public Workshop Workshop { get; set; }
    }
}
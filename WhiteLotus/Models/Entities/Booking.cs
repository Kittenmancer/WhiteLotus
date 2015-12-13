using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Models.Entities
{
    public class Booking
    {
        public virtual int Id { get; set; }
        public virtual int ClassBooked { get; set; }
        public virtual Users BookedBy { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Workshop Workshop { get; set; }
        public virtual bool GonGiveItToYa { get; set; }
    }
}
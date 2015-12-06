using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Models.Entities
{
    public class Workshop
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual int Duration { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string TaughtBy { get; set; }

    }
}
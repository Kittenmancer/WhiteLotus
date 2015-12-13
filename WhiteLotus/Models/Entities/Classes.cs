using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Models.Entities
{
    public class Classes
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Day { get; set; }
        public virtual string StartTime { get; set; }
        public virtual string Duration { get; set; }
        public virtual int Capacity { get; set; }
        public virtual Users TaughtBy { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
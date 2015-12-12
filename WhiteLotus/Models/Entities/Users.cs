using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Models.Entities
{
    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime DOB { get; set; }
        public virtual int Experience { get; set; }
        public virtual bool Health { get; set; }
        public virtual int MOB { get; set; }
        public virtual string Email { get; set; }
        public virtual bool isDeveloper {get; set; }
        public virtual bool isManager { get; set; }
        public virtual string AddressRoad { get; set; }
        public virtual string AddressTown { get; set; }
        public virtual string AdressCounty { get; set; }
        public virtual string AdressPostCode { get; set; }
        public virtual string Password { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual string UserType

        {
            get { return isDeveloper ? "Developer" : (isManager ? " Manager" : "Standard User"); }
        }
    }
}
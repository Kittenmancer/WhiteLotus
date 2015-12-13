using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Models.Entities
{
    public class Users
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
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
        public virtual string Password { get; set; }
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// Check whether the given plaintext password is the valid password for this user.
        /// </summary>
        /// <param name="plainPassword">Plaintext password to check</param>
        /// <returns>Validity of given password for this user</returns>
        public virtual bool CheckPassword(string plainPassword)
        {
            var match = (plainPassword) == Password;
            return match;
        }

        public virtual string UserType

        {
            get { return isDeveloper ? "Developer" : (isManager ? " Manager" : "Standard User"); }
        }
    }
}
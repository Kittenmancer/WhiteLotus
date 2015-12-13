using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;
using WhiteLotus.Models.Mappings;
using WhiteLotus.Models.Queries;
using WhiteLotus.Filters;
using WhiteLotus.Controllers;

namespace WhiteLotus.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<Users> Users { get; set; }
        public Users User { get; set; }
    }
}
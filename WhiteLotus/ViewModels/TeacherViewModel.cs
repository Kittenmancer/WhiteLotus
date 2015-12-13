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
  public  class TeacherViewModel
    { 
            public IEnumerable<Classes> Classes { get; set; }
            public Classes Class { get; set; }
        public Users User { get; set; }
        }
    }
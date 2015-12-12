using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteLotus.Models.Entities;

namespace WhiteLotus
{
    public interface ICurrentUserAware
    {
        Users CurrentUser { get; set; }
    }
}
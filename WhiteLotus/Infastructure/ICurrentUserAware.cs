using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteLotus.Infastructure
{
    public interface ICurrentUserAware
    {
        Models.Entities.Users CurrentUser { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Linq;
using System.Web.Mvc;
using WhiteLotus.Models.Entities;
using WhiteLotus.Filters;
using WhiteLotus;

namespace WhiteLotus.Controllers
{
    [LoadLoggedInUser]
    public abstract class BaseController : Controller, ICurrentUserAware
    {
        public string Notice
        {
            get { return TempData["notice"].ToString(); }
            set { TempData["notice"] = value; }
        }
        public string Error
        {
            get { return TempData["error"].ToString(); }
            set { TempData["error"] = value; }
        }

        public Users CurrentUser { get; set; }

        protected override void OnException(ExceptionContext filterContext)
        {
            Response.Clear();
        }
    }
}
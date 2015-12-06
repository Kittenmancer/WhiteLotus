using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteLotus.Filters;
using WhiteLotus.Models.Entities;

namespace WhiteLotus.Controllers
{
    [LoadLoggedInUser]
    public abstract class BaseController : Controller, Infastructure.ICurrentUserAware
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
            /*new ErrorReporter(filterContext.Exception, filterContext.HttpContext).Send();*/ //come back and implement this later.
        }
    }
}
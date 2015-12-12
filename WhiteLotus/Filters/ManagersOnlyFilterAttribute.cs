using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WhiteLotus.Controllers;

namespace WhiteLotus.Filters
{
    public class ManagersOnlyFilterAttribute : AuthorizeAttribute
    {
        private readonly string _message;

        public ManagersOnlyFilterAttribute() :
            this("You do not have permission to view that page.")
        { }

        public ManagersOnlyFilterAttribute(string message)
        {
            Order = 11;
            _message = message;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;

            if (controller != null && (controller.CurrentUser == null || (!controller.CurrentUser.isManager && !controller.CurrentUser.isDeveloper)))
            {
                // Add a notice if required and redirect to the login page
                if (!string.IsNullOrEmpty(_message))
                    controller.Error = _message;

                filterContext.Result =
                    new RedirectToRouteResult("Login", new RouteValueDictionary());
            }
        }
    }
}
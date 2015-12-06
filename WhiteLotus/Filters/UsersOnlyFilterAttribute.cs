using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WhiteLotus.Controllers;

namespace MapleGroup.Web.Filters {
    public class UsersOnlyFilterAttribute : AuthorizeAttribute {
        private readonly string _message;

        public UsersOnlyFilterAttribute() :
            this("You do not have permission to view that page.") {}

        public UsersOnlyFilterAttribute(string message) {
            Order = 11;
            _message = message;
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            var controller = filterContext.Controller as BaseController;

            if (controller != null && controller.CurrentUser == null) {
                // Add a notice if required and redirect to the login page
                if (!string.IsNullOrEmpty(_message))
                    controller.Error = _message;

                filterContext.Result =
                    new RedirectToRouteResult("Login", new RouteValueDictionary());
            }
        }
    }
}
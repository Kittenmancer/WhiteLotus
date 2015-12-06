using System.Diagnostics;
using System.Web.Mvc;
using WhiteLotus.Models.Entities;
using NHibernate;
using WhiteLotus.Infastructure;

namespace WhiteLotus.Filters
{
    /// <summary>
    /// Loads the currently logged-in user (if any), based on the ID found within the
    /// current ASP.NET session. Allows access without issue if no user is found, the 
    /// CurrentUser property will simply remain untouched.
    /// </summary>
    public class LoadLoggedInUserAttribute : AuthorizeAttribute
    {
        public const string CurrentUserIdKey = "CurrentUserId";

        public ISession Session { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Debug.Assert(filterContext.HttpContext.Session != null, "filterContext.HttpContext.Session != null");
            var userId = filterContext.HttpContext.Session[CurrentUserIdKey] as int?;
            var userAwareController = filterContext.Controller as ICurrentUserAware;

            if (userId != null && userAwareController != null)
            {
                // Load the user from the database
                var user = Session.Get<Users>(userId.Value);
                if (user != null)
                {
                    userAwareController.CurrentUser = user;
                    filterContext.Controller.ViewBag.CurrentUser = user;
                }
            }
        }
    }
}
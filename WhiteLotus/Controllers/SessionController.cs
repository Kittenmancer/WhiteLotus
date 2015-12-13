using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteLotus.Filters;
using WhiteLotus.Models.Entities;
using WhiteLotus.Models.Queries;
using WhiteLotus.ViewModels;
using NHibernate;

namespace WhiteLotus.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ISession _session;

        public SessionController(ISession session)
        {
            _session = session;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string from = null)
        {
            // Retrieve the user based on the username
            var user = new UserFromEmail(username).Execute(_session);

            if (user != null && user.Deleted)
            {
                Error = "That account is currently disabled.";
                return RedirectToAction("Login");
            }

            // If the user exists and the password matches, continue with the login process
            if (user != null && user.CheckPassword(password))
            {
                Session[LoadLoggedInUserAttribute.CurrentUserIdKey] = user.Id;

                // Redirect to the 'from' URI if provided, or the homepage.
                if (!string.IsNullOrEmpty(from))
                    return Redirect(from);
                return RedirectToAction("Index", "Home");
            }

            Error = "You have entered an invalid username or password.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(LoadLoggedInUserAttribute.CurrentUserIdKey);
            return RedirectToAction("Index", "Home");
        }


    }
}

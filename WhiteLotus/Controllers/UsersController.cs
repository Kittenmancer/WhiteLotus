using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteLotus.Filters;
using WhiteLotus.Models.Entities;
using WhiteLotus.Models.Queries;
using WhiteLotus.ViewModels;
using WhiteLotus.Controllers;
using NHibernate;

namespace WhiteLotus.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ISession _session;

        public UsersController(ISession session)
        {
            _session = session;
        }

        [DevelopersOnlyFilter]
        public ActionResult Manage()
        {
            var vM = new UsersViewModel
            {
                Users = _session.QueryOver<Users>().OrderBy(x => x.Deleted).Asc.ThenBy(x => x.Email).Asc.List<Users>()
            };

            return View(vM);
        }

        [DevelopersOnlyFilter]
        public ActionResult Create()
        {
            var vM = new UsersViewModel
            {
                User = new Users()
            };

            return View(vM);
        }

        [HttpPost]
        public ActionResult Create(Users user)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(user);
                tx.Commit();
            }
            Notice = "User account \"" + user.Email + "\" created successfully.";
            return RedirectToAction("Login","Session");
        }

        [DevelopersOnlyFilter]
        public ActionResult Edit(int id)
        {
            var user = _session.Get<Users>(id);

            var vM = new UsersViewModel { User = user };
            return View(vM);
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                using (var tx = _session.BeginTransaction())
                {
                    _session.Update(user);
                    tx.Commit();
                }
                Notice = "User account updated successfully.";
                return RedirectToAction("Manage", "Users");
            }

            var vM = new UsersViewModel
            {
                User = user
            };

            return View(vM);
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _session.Get<Users>(id);
            using (var tx = _session.BeginTransaction())
            {
                user.Deleted = true;
                tx.Commit();
            }
            Notice = "User \"" + user.Email + "\" disabled.";
            return RedirectToAction("Manage");
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult UnDelete(int id)
        {
            var user = _session.Get<Users>(id);
            using (var tx = _session.BeginTransaction())
            {
                user.Deleted = false;
                tx.Commit();
            }
            Notice = "User \"" + user.Email + "\" enabled.";
            return RedirectToAction("Manage");
        }

        [UsersOnlyFilter]
        public ActionResult ChangeOwnPassword()
        {
            var vM = new UsersViewModel
            {
                User = CurrentUser
            };
            return View(vM);
        }
    }
}
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
using WhiteLotus;
using NHibernate;

namespace WhiteLotus.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ISession _session;

        public TeacherController(ISession session)
        {
            _session = session;
        }

        [DevelopersOnlyFilter]
        public ActionResult Manage()
        {
            var vM = new TeacherViewModel
            {
                Classes = _session.QueryOver<Classes>().OrderBy(x => x.Id).Asc.List<Classes>()
            };

            return View(vM);
        }

        [DevelopersOnlyFilter]
        public ActionResult Create()
        {
            var vM = new TeacherViewModel
            {
                Class = new Classes()
            };

            return View(vM);
        }

        [HttpPost]
        public ActionResult Create(Classes classes)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(classes);
                tx.Commit();
            }
            Notice = "Class \"" + classes.Name + "\" created successfully.";
            return RedirectToAction("Login", "Session");
        }

        [DevelopersOnlyFilter]
        public ActionResult Edit(int id)
        {
            var classes = _session.Get<Classes>(id);

            var vM = new TeacherViewModel { Class = classes};
            return View(vM);
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult Edit(Classes classes)
        {
            if (ModelState.IsValid)
            {
                using (var tx = _session.BeginTransaction())
                {
                    _session.Update(classes);
                    tx.Commit();
                }
                Notice = "Class updated successfully.";
                return RedirectToAction("Manage", "Teacher");
            }

            var vM = new TeacherViewModel
            {
                Class = classes
            };

            return View(vM);
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult Delete(int id)
        {
            var classes = _session.Get<Classes>(id);
            using (var tx = _session.BeginTransaction())
            {
                classes.Deleted = true;
                tx.Commit();
            }
            Notice = "Class \"" + classes.Name + "\" cancelled.";
            return RedirectToAction("Manage");
        }

        [DevelopersOnlyFilter, HttpPost]
        public ActionResult UnDelete(int id)
        {
            var classes = _session.Get<Classes>(id);
            using (var tx = _session.BeginTransaction())
            {
                classes.Deleted = false;
                tx.Commit();
            }
            Notice = "Class \"" + classes.Name + "\" rescheduled.";
            return RedirectToAction("Manage");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using WhiteLotus.Filters;
using WhiteLotus.Models.Entities;
using WhiteLotus.ViewModels;


namespace WhiteLotus.Controllers
{
    public class BookingController : BaseController
    {
        public ActionResult Index()
        {
            var vM = new BookingViewModel();
            return View(vM);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteLotus.Models.Entities;
using WhiteLotus.ViewModels;


namespace WhiteLotus.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var vM = new HomeViewModel();
            return View(vM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
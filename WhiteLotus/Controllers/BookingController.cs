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
    public class BookingController : BaseController
    {
        private readonly ISession _session;
        
        public BookingController(ISession session)
        {
            _session = session;


        }
        public ActionResult Index()
        {
            var vM = new BookingViewModel { };
            return View(vM);
        }

        public ActionResult GetBooking(int id)
        {
            var booking = _session.Get<Booking>(id);
            var vM = new BookingViewModel {
                Bookings = new AllBookings().Execute(_session)
            };
            return View("Index",vM);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Booking booking)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(booking);
                tx.Commit();
            }
                booking = new Booking();
            var vM = new BookingViewModel();
            return RedirectToAction("GetBooking");
        }
        [HttpPost, ManagersOnlyFilter, ValidateInput(false)]
        public ActionResult CreateClass(Booking booking)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(booking);
                tx.Commit();
            }
            booking = new Booking();
            var vM = new BookingViewModel();
            return View("Index", vM);
        }
    }
}
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET /home/index
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;
            return View();
        }
        
        //GET /home/about
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //GET /home/contact
        public ActionResult Contact()
        {
            ViewBag.TheMessage = "having Trouble? Contact Us!";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TODO : code message sending
            ViewBag.TheMessage = "Thanks, we got your message";

            return PartialView("_ContactThanks");
        }

        public ActionResult Foo()
        {
            return View("About");
        }

        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVC5ATM1";

            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            //return Content(serial);
            return Json(new { name = "serial", value = serial }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Welcome to my Web App!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Below you will find information for how to contact me.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightSystemWebAPI.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get()
        {
            return new FilePathResult("~/Views/Page/Get.html", "text/html");
        }
    }
}
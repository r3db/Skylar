using System;
using System.Web.Mvc;

namespace Skylar
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
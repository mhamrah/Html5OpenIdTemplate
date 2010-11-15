using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewModel.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

    }
}

using DemoDynamicAuthenticationFilter.App_Start;
using DemoDynamicAuthenticationFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDynamicAuthenticationFilter.Controllers
{
    [ViewName("Home")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizationFilter((int)Priviledge.Read)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AuthorizationFilter((int)Priviledge.Edit)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dabbawala.Models;
using Dabbawala.Util;
using Dabbawala.REST_Client;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Dabbawala.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            string urlLogin = Url.Action("Listar", "Login");
            return RedirectToAction("Listar", "ColetaCarga");
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

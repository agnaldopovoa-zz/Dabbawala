using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dabbawala.Util;
using Microsoft.AspNetCore.Mvc;
using Dabbawala.REST_Client;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Dabbawala.Controllers
{
    public class LoginController : BaseController
    {
        [Route("Login")]
        public IActionResult Login()
        {
            var principal = User as ClaimsPrincipal;
            var check = User.Identity.IsAuthenticated;

            ViewBag.lstPostoTrabalho = "";
            return View("Login");
        }

        [Route("EfetuarLogin")]
        public IActionResult EfetuarLogin(AuthRequest usuario)
        {
            if (AutenticaNaApi(usuario))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }

        private bool AutenticaNaApi(AuthRequest usuario)
        {
            var service = new Service("Auth", "AuthService", "RequestToken", Method.POST);
            var request = service.GetRequest();

            if ((string.IsNullOrEmpty(usuario.UserName)) || (string.IsNullOrEmpty(usuario.Password)))
                return false;

            request.AddJsonBody(usuario);
            var response = service.Client.Execute(request);

           JObject jObject = JObject.Parse(response.Content);
            string token = (string)jObject.SelectToken("token");

            Identity.SetToken(HttpContext, token);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}

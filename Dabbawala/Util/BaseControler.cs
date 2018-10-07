using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dabbawala.Util
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.RouteData.Values["action"] != null &&
                (filterContext.RouteData.Values["action"].ToString().ToLower() == "login" ||
                 filterContext.RouteData.Values["action"].ToString().ToLower() == "efetuarlogin"))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                var token = HttpContext.Session.GetString("access_token");
                var check = !string.IsNullOrWhiteSpace(token);
        
                if (!check)
                {
                    string urlLogin = Url.Action("Login", "Login");
                    Response.Redirect(urlLogin);
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        //public STCException ObterModelError()
        //{
        //    lsterros = new List<string>();

        //    StringBuilder str = new StringBuilder();

        //    foreach (var modelState in ViewData.ModelState.Values)
        //    {
        //        foreach (ModelError error in modelState.Errors)
        //        {
        //            //str.Append(Environment.NewLine);
        //            //str.Append(error.ErrorMessage);
        //            lsterros.Add(error.ErrorMessage);
        //        }
        //    }

        //    STCException ex = new STCException("DOC_04", STCExceptionEnum.Erro);

        //    return ex;
        //}
    }
}

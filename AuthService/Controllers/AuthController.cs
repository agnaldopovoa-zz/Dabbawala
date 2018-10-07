using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthService.Models;
using AuthService.Utils;
using Microsoft.Extensions.Logging;

namespace AuthService.Controllers
{
    [Route("dabbawala/AuthService")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RequestToken")]
        public IActionResult RequestToken(AuthRequest request)
        {
            //Kept it simple for demo purpose
            //var user = new OperadoresControlador().ObterNoTracking(P => P.OpMat == request.UserName && P.OpSenha == request.Password);
            //if (user == null) return BadRequest("Invalid credentials.");
            //var token = new TokenUtility().GenerateToken(user.OpMat, user.OpIdOp.ToString());
            var tok = new TokenUtility().GenerateToken("user", "1");
            var result = Ok(new
            {
                token = tok
            });

            return result;
        }
    }
}
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Utils
{
    public class TokenUtility
    {
        //Em produção deveria ser assim
        //string jwtKey = Environment.GetEnvironmentVariable("JwtKey")

        //string TokenIssuer = "teste.com";

        public string GenerateToken(string userName, string userId)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userId, "Token"),
                new[]
                {
                    new Claim("ID",userId)
                }
                );

            var keyByteArray = Encoding.ASCII.GetBytes("f10arm3345-42346-fsd3-fsdg44afe");
            var signinKey = new SymmetricSecurityKey(keyByteArray);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = DateTime.Now.Add(TimeSpan.FromDays(1)),
                NotBefore = DateTime.Now
            });

            var retorno = handler.WriteToken(securityToken);

            return retorno;
        }
    }
}

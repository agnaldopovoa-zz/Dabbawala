using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dabbawala.REST_Client
{
    public class Identity
    {
        private static readonly string tokenName = "access_token";

        /// <summary>
        /// Verifica se o usuário está logado
        /// </summary>
        /// <param name="contexto">Contexto</param>
        /// <returns></returns>
        public static bool IsLogged(HttpContext contexto)
        {
            var token = contexto.Session.GetString(tokenName);

            return (!string.IsNullOrWhiteSpace(token));
        }
        /// <summary>
        /// Obtém o token
        /// </summary>
        /// <param name="contexto">Contexto</param>
        /// <returns></returns>
        public static string GetToken(HttpContext contexto)
        {
            var token = contexto.Session.GetString(tokenName);

            return token;
        }
        /// <summary>
        /// Registra o token na session
        /// </summary>
        /// <param name="contexto">Contexto</param>
        /// <param name="token">Token gerado pelo REST Web API</param>
        public static void SetToken(HttpContext contexto, string token)
        {
            contexto.Session.SetString(tokenName, token);
        }

        /// <summary>
        /// Limpa o token na session
        /// </summary>
        /// <param name="contexto">Contexto</param>
        public static void ClearToken(HttpContext contexto)
        {
            contexto.Session.SetString(tokenName, "");
        }
    }
}

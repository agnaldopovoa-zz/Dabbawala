using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIClient;
using Persistencia.Entities;

namespace ColetaCargaService
{
    public class HttpApiExpedicao
    {
        public static async Task<Cliente> GetUsers()
        {
            var httpApiClient = new HttpClientAPI(new Uri("https://localhost:5011/dabbawala/Expedicao/"));
            var requestUrl = httpApiClient.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "GetExpedicao/2"));
            var c = await httpApiClient.GetAsync<Cliente>(requestUrl);
            return c;
        }

        public static async Task<Cliente> SaveUser(Cliente model)
        {
            var httpApiClient = new HttpClientAPI(new Uri("https://localhost:5011/dabbawala/Expedicao/"));
            var requestUrl = httpApiClient.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "User/SaveUser"));
            return await httpApiClient.PostAsync<Cliente>(requestUrl, model);
        }
    }
}

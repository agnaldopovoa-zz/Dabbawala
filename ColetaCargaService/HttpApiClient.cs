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
        public static async void EnviarExpedicao(long _idSolcitacao)
        {
            var httpApiClient = new HttpClientAPI(new Uri("https://localhost:5011/dabbawala/Expedicao/"));
            var requestUrl = httpApiClient.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "NovaExpedicao/" + _idSolcitacao.ToString()));
            var c = await httpApiClient.PostAsync<IAsyncResult>(requestUrl, null);
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

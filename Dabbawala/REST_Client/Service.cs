using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace Dabbawala.REST_Client
{
    public class Service : RestRequest
    {
        private readonly Dictionary<string, RestClient> Clients = new Dictionary<string, RestClient>()
        {
            {"auth", new RestClient("https://localhost:5021/dabbawala/")},
            {"coletacarga", new RestClient("https://localhost:5001/dabbawala/")},
            {"expedicao", new RestClient("https://localhost:5011/dabbawala/")}
        };

        private RestClient client;
        public RestClient Client { get => client; }

        public Service(string serviceName, string controller, string action, Method method)
        {
            //Clients.Add("Auth", new RestClient("http://localhost:5021/dabbawala/"));
            client = Clients[serviceName.ToLower()];

            base.Resource = controller + "/" + action;
            base.Method = method;
        }

        public IRestResponse ExecuteWithToken(IRestRequest request, HttpContext context)
        {
            request.AddParameter("Authorization", string.Format("Bearer " + Identity.GetToken(context)), ParameterType.HttpHeader);

            return client.Execute(request);
        }

        public IRestResponse<T> ExecuteWithToken<T>(IRestRequest request, HttpContext context) where T : new()
        {
            request.AddParameter("Authorization", string.Format("Bearer " + Identity.GetToken(context)), ParameterType.HttpHeader);

            return client.Execute<T>(request);
        }

        public RestRequest GetRequest()
        {
            RestRequest request = new RestRequest(base.Resource, base.Method);

            return request;
        }
    }
}

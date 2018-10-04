using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Persistencia.DAL;
// using Persistencia.DTO;
using Persistencia.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpedicaoService.Controllers
{
    [Route("dabbawala/Expedicao")]
    [ApiController]
    public class ExpedicaoController : Controller
    {
        private readonly dabbawalaContext _context;

        public ExpedicaoController(dabbawalaContext context)
        {
            _context = context;
        }

        // GET: dabbawala/Expedicao
        [HttpGet]
        //[ActionName("Index")]
        public HttpResponseMessage DescribeAPI()
        {
            string result = @"
                  <title>API Expedição</title>
                  <h1> API Módulo Expedição </h1>
                  <p>Hello There</p>
                  ";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }


        // GET: dabbawala/Expedicao/
        [HttpGet("GetExpedicao/{id}", Name = "GetExpedicao")]
        public ActionResult<Cliente> GetExpedicao(long id)
        {
            try
            {
                Cliente expedicao = new ClienteDAL().Obter(id);

                if (expedicao == null)
                    return NotFound("Não foi encontrada expedição com o ID informado [" + id.ToString() + "]");

                return expedicao;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Produces("text/html")]
        //public string DescribeAPI()
        //{
        //    string response = @"
        //      <title>API Expedição</title>
        //      <h1> API Módulo Expedição </h1>
        //      <p>Hello There <button>click me</button></p>
        //      ";
        //    return response;
        //}

        // POST dabbawala/Expedicao/<value>
        [HttpPost("NovaExpedicao/{id}", Name = "NovaExpedicao")]
        public IActionResult NovaExpedicao(int id)
        {
            // Converte o parâmetro de entrada
            //long id = 0;
            //if (!Int64.TryParse(value, out id))
            //    return BadRequest();

            // Chama o DAL para a inserção da nova expedição
            ExpedicaoDAL expedicao = new ExpedicaoDAL();
            try
            {
                if (expedicao.CriarNovaExpedicao(id) != null)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT dabbawala/Expedicao/<id>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE dabbawala/Expedicao/<id>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

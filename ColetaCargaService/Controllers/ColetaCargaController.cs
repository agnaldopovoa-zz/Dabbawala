using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DAL;
//using Persistencia.DTO;
using Persistencia.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColetaCargaService.Controllers
{
    [Route("dabbawala/ColetaCarga")]
    [ApiController]
    public class ColetaCargaController : Controller
    {
        private readonly dabbawalaContext context;

        public ColetaCargaController(dabbawalaContext _context)
        {
            context = _context;
        }

        // GET: dabbawala/ColetaCarga
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            return context.Produto.ToList();
        }

        // GET: dabbawala/ColetaCarga/<entidade>
        [HttpGet("ListarEntidade/{entidade}", Name = "ListarEntidade")]
        public string GetEntity(string entidade)
        {
            return EntidadesDAL.GetEntityByName(context, entidade);
        }

        // GET api/<controller>/id
        //[HttpGet("{id}", Name ="ObterColeta")]
        //public ActionResult<Coleta> GetById(long id)
        //{
        //    var item = _context.Coletas.Find(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return item;
        //}

        // POST api/<controller>
        [HttpPost("NovaColeta", Name = "NovaColeta")]
        public IActionResult criarNovaColeta([FromBody]InformacoesColetaDTO value)
        {
            try
            {
                SolicitacaoTransporteDAL solicitacao = new SolicitacaoTransporteDAL();
                if (solicitacao.CriarSolicitacaoTransporte(value) != null)
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

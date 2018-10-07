using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DAL;
using Persistencia.Entities;
using APIClient;

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
        [HttpGet("Listar", Name = "Listar")]
        public ActionResult<List<InformacoesColetaDTO>> GetAll()
        {
            ///return DALControl<Produto>().Listar();
            var solicitacaoDAL = new SolicitacaoTransporteDAL();
            return solicitacaoDAL.ObterSolicitacoesTransporte();
        }

        // GET: dabbawala/ColetaCarga
        [HttpGet("Obter/{id}", Name = "Obter")]
        public ActionResult<InformacoesColetaDTO> Get(long id)
        {
            var solicitacaoDAL = new SolicitacaoTransporteDAL();
            return solicitacaoDAL.ObterSolicitacaoTransporte(id);
        }

        // GET: dabbawala/ColetaCarga/<entidade>
        [HttpGet("ListarEntidade/{entidade}", Name = "ListarEntidade")]
        public string GetEntity(string entidade)
        {
            return EntidadesDAL.GetEntityByName(context, entidade);
        }

        // POST dabbawala/ColetaCarga
        [HttpPost("NovaColeta", Name = "NovaColeta")]
        public IActionResult CriarNovaColeta([FromBody]InformacoesColetaDTO value)
        {
            try
            {
                SolicitacaoTransporteDAL solicitacaoDAL = new SolicitacaoTransporteDAL();
                SolicitacaoTransporte solicitacao = solicitacaoDAL.CriarSolicitacaoTransporte(value);
                if (solicitacao != null)
                {
                    HttpApiExpedicao.EnviarExpedicao(solicitacao.IdSolicitacao);
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT dabbawala/ColetaCarga/Editar/{id}
        [HttpPut("Editar/{id}", Name = "Editar")]
        public IActionResult Put(int id, [FromBody]InformacoesColetaDTO value)
        {
            try
            {
                SolicitacaoTransporteDAL solicitacao = new SolicitacaoTransporteDAL();
                solicitacao.EditarSolicitacaoTransporte(value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE dabbawala/ColetaCarga/Remover/{id}
        [HttpDelete("Remover/{id}", Name = "Remover")]
        public IActionResult Delete(int id)
        {
            try
            {
                SolicitacaoTransporteDAL solicitacao = new SolicitacaoTransporteDAL();
                solicitacao.RemoverSolicitacaoTransporte(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

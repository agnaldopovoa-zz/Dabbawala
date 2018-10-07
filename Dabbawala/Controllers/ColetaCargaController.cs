using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dabbawala.Models;
using Dabbawala.Util;
using Dabbawala.REST_Client;
using Newtonsoft.Json;
using RestSharp;

namespace Dabbawala.Controllers
{
    public class ColetaCargaController : BaseController
    {
        // ---------------------------------------------------------------------------------
        //                   Criar
        // ---------------------------------------------------------------------------------
        public IActionResult Listar()
        {
            List<SolicitacaoTransporteResponse> model = BuscarSolicitacoes();
            return View("../ColetaCarga/Listar", model);
        }


        // ---------------------------------------------------------------------------------
        //                   C R I A R
        // ---------------------------------------------------------------------------------
        public IActionResult Criar(SolicitacaoTransporteResponse solicitacao)
        {
            return View("../ColetaCarga/Criar");
        }

        public IActionResult ExecutarCriacao(SolicitacaoTransporteResponse solicitacao,
            SolicitacaoTransporteResponse.InformacoesItemsDTO item)
        {
            if (CriarSolicitacao(solicitacao, item))
                return RedirectToAction("Listar", "ColetaCarga");
            else
                return BadRequest();
        }

        //public ActionResult Itens(SolicitacaoTransporteResponse.InformacoesItemsDTO itens)
        //{
        //    return PartialView(itens);
        //}
        // ---------------------------------------------------------------------------------


        // ---------------------------------------------------------------------------------
        //                   E D I T A R
        // ---------------------------------------------------------------------------------
        public IActionResult Editar(SolicitacaoTransporteResponse solicitacao)
        {
            var service = new Service("Coletacarga", "Coletacarga", "Obter/" + solicitacao.ID.ToString(), Method.GET);
            var request = service.GetRequest();

            //request.AddJsonBody("");
            var response = service.Client.Execute(request);
            SolicitacaoTransporteResponse model = JsonConvert.DeserializeObject<SolicitacaoTransporteResponse>(response.Content);
            List<SolicitacaoTransporteResponse.InformacoesItemsDTO> modelDetail = model.Itens.ToList();
            return View("../ColetaCarga/Editar", model);
        }

        public IActionResult ExecutarEdicao(SolicitacaoTransporteResponse solicitacao,
            SolicitacaoTransporteResponse.InformacoesItemsDTO item)
        {
            if (EditarSolicitacao(solicitacao, item))
                return RedirectToAction("Editar", "ColetaCarga", solicitacao);
            else
                return BadRequest();
        }


        // ---------------------------------------------------------------------------------
        //                   R E M O V E R
        // ---------------------------------------------------------------------------------
        public IActionResult Remover(SolicitacaoTransporteResponse solicitacao)
        {
            //var service = new Service("Coletacarga", "Coletacarga", "Obter/" + solicitacao.ID.ToString(), Method.GET);
            //var request = service.GetRequest();

            //var response = service.Client.Execute(request);
            //SolicitacaoTransporteResponse model = JsonConvert.DeserializeObject<SolicitacaoTransporteResponse>(response.Content);
            return View("../ColetaCarga/Remover", solicitacao);
        }

        public IActionResult ExecutarRemocao(SolicitacaoTransporteResponse solicitacao)
        {
            if (RemoverSolicitacao(solicitacao))
                return RedirectToAction("Listar", "ColetaCarga");
            else
                return BadRequest();
        }


        // ---------------------------------------------------------------------------------
        private List<SolicitacaoTransporteResponse> BuscarSolicitacoes()
        {
            List<SolicitacaoTransporteResponse> solicitacoes = null;

            var service = new Service("Coletacarga", "Coletacarga", "Listar", Method.GET);
            var request = service.GetRequest();

            var response = service.Client.Execute(request);

            solicitacoes = JsonConvert.DeserializeObject<List<SolicitacaoTransporteResponse>>(response.Content);

            return solicitacoes;
        }

        private bool CriarSolicitacao(SolicitacaoTransporteResponse solicitacao,
            SolicitacaoTransporteResponse.InformacoesItemsDTO item)
        {
            var service = new Service("ColetaCarga", "Coletacarga", "NovaColeta", Method.POST);
            var request = service.GetRequest();

            List<SolicitacaoTransporteResponse.InformacoesItemsDTO> items = new List<SolicitacaoTransporteResponse.InformacoesItemsDTO>();
            items.Add(item);
            solicitacao.Itens = items;

            request.AddJsonBody(solicitacao);
            var response = service.Client.Execute(request);

            var errorMessage = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private bool EditarSolicitacao(SolicitacaoTransporteResponse solicitacao,
            SolicitacaoTransporteResponse.InformacoesItemsDTO item)
        {
            var service = new Service("ColetaCarga", "Coletacarga", "Editar/" + solicitacao.ID.ToString(), Method.PUT);
            var request = service.GetRequest();

            List<SolicitacaoTransporteResponse.InformacoesItemsDTO> items = new List<SolicitacaoTransporteResponse.InformacoesItemsDTO>();
            items.Add(item);
            solicitacao.Itens = items;

            request.AddJsonBody(solicitacao);
            var response = service.Client.Execute(request);

            var errorMessage = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private bool RemoverSolicitacao(SolicitacaoTransporteResponse solicitacao)
        {
            var service = new Service("ColetaCarga", "Coletacarga", "Remover/" + solicitacao.ID.ToString(), Method.DELETE);
            var request = service.GetRequest();

            var response = service.Client.Execute(request);

            var errorMessage = response.Content;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
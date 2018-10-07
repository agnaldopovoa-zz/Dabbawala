using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Entities;
using System.Linq;

namespace Persistencia.DAL
{
    public class ItensSolicitacaoDAL : DALControl<ItensSolicitacao>
    {
        public List<InformacoesItemsDTO> GetInformacoesItensSolicitacao(long _idSolicitacao)
        {
            var query = (from i in db.ItensSolicitacao
                         where i.IdSolicitacao == _idSolicitacao
                         select new
                         {
                             id = i.IdItemSolicitacao,
                             idSolcitacao = i.IdSolicitacao,
                             idProduto = i.IdProduto,
                             quantidade = i.Quantidade,
                             idUnidade = i.IdUnidade
                         }).ToList().Select(x => new InformacoesItemsDTO()
                         {
                             ID = x.id,
                             Produto = new DALControl<Produto>().Obter(x.idProduto).Descricao, //x.idProduto,
                             Quantidade = x.quantidade.ToString(),
                             Unidade = new DALControl<Unidade>().Obter(x.idUnidade).Sigla
                         }).ToList();

            return query;
        }
    }
}

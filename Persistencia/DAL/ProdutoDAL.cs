using Persistencia.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Persistencia.DAL
{
    public class ProdutoDAL : DALControl<Produto>
    {
        public Produto GetProduto(string _descricao)
        {
            Produto produto = null;
            string descricao = _descricao.Trim().ToUpper();

            var query = (from p in db.Produto
                         where p.Descricao.Trim().ToUpper().Contains(descricao)
                         select new
                         {
                             idproduto = p.IdProduto,
                             descricao = p.Descricao,
                             ncm = p.CodigoNcm,
                             idembalagem = p.IdEmbalagem,
                             perecivel = p.Perecivel,
                             perigoso = p.Perigoso,
                             qtdeporembalagem = p.QtdePorEmbalagem
                         }).ToList().Select(x => new Produto()
                         {
                             IdProduto = x.idproduto,
                             Descricao = x.descricao,
                             CodigoNcm = x.ncm,
                             IdEmbalagem = x.idembalagem,
                             Perecivel = x.perecivel,
                             Perigoso = x.perigoso,
                             QtdePorEmbalagem = x.qtdeporembalagem
                         }).ToList();

            if (query.Count == 1)
                produto = query.First();

            return produto;
        }
    }
}

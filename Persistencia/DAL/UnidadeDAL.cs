using Persistencia.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Persistencia.DAL
{
    public class UnidadeDAL : DALControl<UnidadeDAL>
    {
        public Unidade GetUnidade(string _descricao)
        {
            Unidade unidade = null;
            string descricao = _descricao.Trim().ToUpper();

            var query = (from u in db.Unidade
                         where u.Sigla.Trim().ToUpper().Contains(descricao)
                         select new
                         {
                             idUnidade = u.IdUnidade,
                             descricao = u.Descricao,
                             sigla = u.Sigla
                         }).ToList().Select(x => new Unidade()
                         {
                             IdUnidade = x.idUnidade,
                             Descricao = x.descricao,
                             Sigla = x.sigla
                         }).ToList();

            if (query.Count == 1)
                unidade = query.First();

            return unidade;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Persistencia.Entities;

namespace Persistencia.DAL
{
    public class EnderecoDAL : DALControl<Endereco>
    {
        public Endereco GetEndereco(string _logradouro,
                                    string _numero,
                                    string _bairro,
                                    string _municipio,
                                    string _uf)
        {
            Endereco endereco = null;
            string logradouro = _logradouro.Trim().ToUpper();
            string numero = _numero.Trim().ToUpper();
            string bairro = _bairro.Trim().ToUpper();
            string municipio = _municipio.Trim().ToUpper();
            string uf = _uf.Trim().ToUpper();

            var query = (from e in db.Endereco
                         join b in db.Bairro on e.IdBairro equals b.IdBairro
                         join m in db.Municipio on b.IdMunicipio equals m.IdMunicipio
                         join u in db.Estado on m.IdEstado equals u.IdEstado
                         where (e.Logradouro.Trim().ToUpper() == logradouro)
                            && (e.Numero == numero)
                            && (b.Nome == bairro)
                            && (m.Nome == municipio)
                            && (u.Sigla == uf)
                         select new
                         {
                             idEndereco = e.IdEndereco
                         }).ToList().Select(x => new Endereco()
                         {
                             IdEndereco = x.idEndereco,
                         }).ToList();

            if (query.Count == 1)
                endereco = query.First();

            return endereco;
        }
    }
}

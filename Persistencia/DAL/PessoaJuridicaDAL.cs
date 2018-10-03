using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Entities;

namespace Persistencia.DAL
{
    public class PessoaJuridicaDAL : DALControl<PessoaJuridica>
    {
        public PessoaJuridica GetByCNPJ(string _CNPJ)
        {
            long cnpj = Int64.Parse(_CNPJ);
            PessoaJuridica pessoaJuridica = Obter(pj => pj.Cnpj.Equals(cnpj));
            return pessoaJuridica;
        }
    }
}

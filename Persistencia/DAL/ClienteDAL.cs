using Persistencia.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DAL
{
    public class ClienteDAL : DALControl<Cliente>
    {
        public Cliente GetByCNPJ(string CNPJ)
        {
            Cliente cliente = null;

            PessoaJuridica pessoaJuridica = new PessoaJuridicaDAL().GetByCNPJ(CNPJ);
            if (pessoaJuridica != null)
                cliente = Obter(c => c.IdPessoa == pessoaJuridica.IdPessoa);

            return cliente;
        }
    }
}

using Persistencia.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.DAL
{
    public class FornecedorDAL : DALControl<Fornecedor>
    {
        public Fornecedor GetByCNPJ(string CNPJ)
        {
            Fornecedor fornecedor = null;

            PessoaJuridica pessoaJuridica = new PessoaJuridicaDAL().GetByCNPJ(CNPJ);
            if (pessoaJuridica != null)
                fornecedor = Obter(c => c.IdPessoa == pessoaJuridica.IdPessoa);

            return fornecedor;
        }
    }
}

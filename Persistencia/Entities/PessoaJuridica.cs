using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class PessoaJuridica
    {
        public PessoaJuridica()
        {
            SolicitacaoTransporte = new HashSet<SolicitacaoTransporte>();
        }

        public long IdPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public decimal Cnpj { get; set; }
        public long IdEndereco { get; set; }
        public long? IdEnderecoEletronico { get; set; }

        public EnderecoEletronico IdEnderecoEletronicoNavigation { get; set; }
        public Endereco IdEnderecoNavigation { get; set; }
        public Cliente Cliente { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public ICollection<SolicitacaoTransporte> SolicitacaoTransporte { get; set; }
    }
}

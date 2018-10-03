using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Endereco
    {
        public Endereco()
        {
            PessoaJuridica = new HashSet<PessoaJuridica>();
            SolicitacaoTransporteIdLocalColetaNavigation = new HashSet<SolicitacaoTransporte>();
            SolicitacaoTransporteIdLocalEntregaNavigation = new HashSet<SolicitacaoTransporte>();
        }

        public long IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public long? IdBairro { get; set; }

        public Bairro IdBairroNavigation { get; set; }
        public ICollection<PessoaJuridica> PessoaJuridica { get; set; }
        public ICollection<SolicitacaoTransporte> SolicitacaoTransporteIdLocalColetaNavigation { get; set; }
        public ICollection<SolicitacaoTransporte> SolicitacaoTransporteIdLocalEntregaNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class SolicitacaoTransporte
    {
        public SolicitacaoTransporte()
        {
            Expedicao = new HashSet<Expedicao>();
            ItensSolicitacao = new HashSet<ItensSolicitacao>();
        }

        public long IdSolicitacao { get; set; }
        public long IdCliente { get; set; }
        public long IdLocalColeta { get; set; }
        public long IdDestinatario { get; set; }
        public long IdLocalEntrega { get; set; }
        public long? IdTransportador { get; set; }
        public string Observacoes { get; set; }

        public Cliente IdClienteNavigation { get; set; }
        public PessoaJuridica IdDestinatarioNavigation { get; set; }
        public Endereco IdLocalColetaNavigation { get; set; }
        public Endereco IdLocalEntregaNavigation { get; set; }
        public Fornecedor IdTransportadorNavigation { get; set; }
        public ICollection<Expedicao> Expedicao { get; set; }
        public ICollection<ItensSolicitacao> ItensSolicitacao { get; set; }
    }
}

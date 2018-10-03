using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            SolicitacaoTransporte = new HashSet<SolicitacaoTransporte>();
        }

        public long IdPessoa { get; set; }
        public bool? FaturaOrigem { get; set; }
        public bool? Bloqueado { get; set; }

        public PessoaJuridica IdPessoaNavigation { get; set; }
        public ICollection<SolicitacaoTransporte> SolicitacaoTransporte { get; set; }
    }
}

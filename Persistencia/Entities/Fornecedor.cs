using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            SolicitacaoTransporte = new HashSet<SolicitacaoTransporte>();
        }

        public long IdPessoa { get; set; }
        public bool? Interestadual { get; set; }
        public bool? CargaPerigosa { get; set; }
        public bool? CargaPerecível { get; set; }

        public PessoaJuridica IdPessoaNavigation { get; set; }
        public ICollection<SolicitacaoTransporte> SolicitacaoTransporte { get; set; }
    }
}

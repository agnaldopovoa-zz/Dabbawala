using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Expedicao
    {
        public long IdExpedicao { get; set; }
        public long? IdSolicitacao { get; set; }
        public string Status { get; set; }
        public DateTime? PrevisaoEntrega { get; set; }

        public SolicitacaoTransporte IdSolicitacaoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Unidade
    {
        public Unidade()
        {
            ItensSolicitacao = new HashSet<ItensSolicitacao>();
        }

        public long IdUnidade { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public ICollection<ItensSolicitacao> ItensSolicitacao { get; set; }
    }
}

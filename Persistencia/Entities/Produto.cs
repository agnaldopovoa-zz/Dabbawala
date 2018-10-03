using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Produto
    {
        public Produto()
        {
            ItensSolicitacao = new HashSet<ItensSolicitacao>();
        }

        public long IdProduto { get; set; }
        public string Descricao { get; set; }
        public string CodigoNcm { get; set; }
        public long? IdEmbalagem { get; set; }
        public int? QtdePorEmbalagem { get; set; }
        public bool? Perigoso { get; set; }
        public bool? Perecivel { get; set; }

        public Embalagem IdEmbalagemNavigation { get; set; }
        public ICollection<ItensSolicitacao> ItensSolicitacao { get; set; }
    }
}

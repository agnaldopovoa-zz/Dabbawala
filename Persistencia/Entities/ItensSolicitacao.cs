using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class ItensSolicitacao
    {
        public long IdItemSolicitacao { get; set; }
        public long IdSolicitacao { get; set; }
        public long IdProduto { get; set; }
        public long? IdUnidade { get; set; }
        public int? Quantidade { get; set; }

        public Produto IdProdutoNavigation { get; set; }
        public SolicitacaoTransporte IdSolicitacaoNavigation { get; set; }
        public Unidade IdUnidadeNavigation { get; set; }
    }
}

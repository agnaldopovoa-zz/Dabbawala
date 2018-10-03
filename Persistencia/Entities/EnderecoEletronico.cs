using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class EnderecoEletronico
    {
        public EnderecoEletronico()
        {
            PessoaJuridica = new HashSet<PessoaJuridica>();
        }

        public long IdEnderecoEletronico { get; set; }
        public long IdTipendEletronico { get; set; }
        public string Descricao { get; set; }

        public TipoEnderecoEletronico IdTipendEletronicoNavigation { get; set; }
        public ICollection<PessoaJuridica> PessoaJuridica { get; set; }
    }
}

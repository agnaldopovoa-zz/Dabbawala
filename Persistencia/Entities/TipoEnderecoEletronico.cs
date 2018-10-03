using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class TipoEnderecoEletronico
    {
        public TipoEnderecoEletronico()
        {
            EnderecoEletronico = new HashSet<EnderecoEletronico>();
        }

        public long IdTipendEletronico { get; set; }
        public string Nome { get; set; }

        public ICollection<EnderecoEletronico> EnderecoEletronico { get; set; }
    }
}

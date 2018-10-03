using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Embalagem
    {
        public Embalagem()
        {
            Produto = new HashSet<Produto>();
        }

        public long IdEmbalagem { get; set; }
        public string Descricao { get; set; }
        public int? EmpilhamentoMaximo { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}

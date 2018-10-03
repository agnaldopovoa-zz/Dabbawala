using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Estado
    {
        public Estado()
        {
            Municipio = new HashSet<Municipio>();
        }

        public long IdEstado { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }

        public ICollection<Municipio> Municipio { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Municipio
    {
        public Municipio()
        {
            Bairro = new HashSet<Bairro>();
        }

        public long IdMunicipio { get; set; }
        public long? IdEstado { get; set; }
        public string Nome { get; set; }
        public decimal? CodigoIbge { get; set; }

        public Estado IdEstadoNavigation { get; set; }
        public ICollection<Bairro> Bairro { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Persistencia.Entities
{
    public partial class Bairro
    {
        public Bairro()
        {
            Endereco = new HashSet<Endereco>();
        }

        public long IdBairro { get; set; }
        public long? IdMunicipio { get; set; }
        public string Nome { get; set; }

        public Municipio IdMunicipioNavigation { get; set; }
        public ICollection<Endereco> Endereco { get; set; }
    }
}

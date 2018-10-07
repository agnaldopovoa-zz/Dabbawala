using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dabbawala.Models
{
    public class SolicitacaoTransporteResponse
    {
        public class EnderecoColetaDTO
        {
            [Key]
            [Display(Name = "ID")]
            public long ID { get; set; }
            [Display(Name = "Logradouro")]
            public string Logradouro { get; set; }
            [Display(Name = "Número")]
            public string Numero { get; set; }
            [Display(Name = "Bairro")]
            public string Bairro { get; set; }
            [Display(Name = "Município")]
            public string Municipio { get; set; }
            [Display(Name = "UF")]
            public string UF { get; set; }
        }

        public class InformacoesItemsDTO
        {
            [Key]
            [Display(Name = "ID")]
            public long ID { get; set; }
            [Display(Name = "Produto")]
            public string Produto { get; set; }
            [Display(Name = "Unidade")]
            public string Unidade { get; set; }
            [Display(Name = "Quantidade")]
            public string Quantidade { get; set; }
        }

        [Key]
        [Display(Name = "ID")]
        public long ID { get; set; }
        [Display(Name = "CNPJ Cliente")]
        public string CNPJCliente { get; set; }
        public EnderecoColetaDTO LocalColeta { get; set; }
        [Display(Name = "CNPJ Destinatário")]
        public string CNPJDestinatario { get; set; }
        public EnderecoColetaDTO LocalEntrega { get; set; }
        [Display(Name = "CNPJ Transportador")]
        public string CNPJTransportador { get; set; }
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
        public ICollection<InformacoesItemsDTO> Itens { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Persistencia.DTO;
using Persistencia.Entities;

namespace Persistencia.DAL
{
    #region Classes utilizadas para recebimento de nova coleta
    public class EnderecoColetaDTO
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
    }

    public class InformacoesItemsDTO
    {
        public string Produto { get; set; }
        public string Unidade { get; set; }
        public string Quantidade { get; set; }
    }

    public class InformacoesColetaDTO
    {
        public string CNPJCliente { get; set; }
        public EnderecoColetaDTO LocalColeta { get; set; }
        public string CNPJDestinatario { get; set; }
        public EnderecoColetaDTO LocalEntrega { get; set; }
        public string CNPJTransportador { get; set; }
        public string Observacoes { get; set; }
        public ICollection<InformacoesItemsDTO> Itens { get; set; }
    }
    #endregion

    public class SolicitacaoTransporteDAL : DALControl<SolicitacaoTransporte>
    {
        public SolicitacaoTransporte CriarSolicitacaoTransporte(InformacoesColetaDTO _infoColeta)
        {

            using (var dbs = new dabbawalaContext())
            {
                //  Busca o cliente
                Cliente cliente = new ClienteDAL().GetByCNPJ(_infoColeta.CNPJCliente);
                if (cliente == null)
                    throw new Exception("Não foi encontrado cliente com o CNPJ informado");

                //  Busca o destinatário
                PessoaJuridica destinatario = new PessoaJuridicaDAL().GetByCNPJ(_infoColeta.CNPJDestinatario);
                if (destinatario == null)
                    throw new Exception("Não foi encontrado destinatário com o CNPJ informado");

                // Busca o local de coleta;
                Endereco enderecoColeta = new EnderecoDAL().GetEndereco(_infoColeta.LocalColeta.Logradouro,
                                                                       _infoColeta.LocalColeta.Numero,
                                                                       _infoColeta.LocalColeta.Bairro,
                                                                       _infoColeta.LocalColeta.Municipio,
                                                                       _infoColeta.LocalColeta.UF);
                if (enderecoColeta == null)
                    throw new Exception("Não foi encontrado o endereco de coleta informado");

                // Busca o local de coleta;
                Endereco enderecoEntrega = new EnderecoDAL().GetEndereco(_infoColeta.LocalEntrega.Logradouro,
                                                                        _infoColeta.LocalEntrega.Numero,
                                                                        _infoColeta.LocalEntrega.Bairro,
                                                                        _infoColeta.LocalEntrega.Municipio,
                                                                        _infoColeta.LocalEntrega.UF);
                if (enderecoEntrega == null)
                    throw new Exception("Não foi encontrado o endereco de coleta informado");

                using (var trans = dbs.Database.BeginTransaction())
                {
                    var solicitacao = new SolicitacaoTransporte
                    {
                        // IdSolicitacao = dbs.SolicitacaoTransporte.Select(s => s.IdSolicitacao).DefaultIfEmpty(0).Max(),
                        IdCliente = cliente.IdPessoa,
                        IdLocalColeta = enderecoColeta.IdEndereco,
                        IdDestinatario = destinatario.IdPessoa,
                        IdLocalEntrega = enderecoEntrega.IdEndereco,
                        IdTransportador = null,
                        Observacoes = _infoColeta.Observacoes
                    };

                    if (_infoColeta.Itens != null)
                    {
                        foreach (InformacoesItemsDTO infoItemDTO in _infoColeta.Itens)
                        {
                            // Busca o produto;
                            Produto produto = new ProdutoDAL().GetProduto(infoItemDTO.Produto);
                            if (produto == null)
                                throw new Exception("Não foi encontrado o produto informado");

                            // Busca a unidade;
                            Unidade unidade = new UnidadeDAL().GetUnidade(infoItemDTO.Unidade);
                            if (unidade == null)
                                throw new Exception("Não foi encontrada a unidade informada");

                            int qtde = 0;
                            Int32.TryParse(infoItemDTO.Quantidade, out qtde);
                            var item = new ItensSolicitacao
                            {
                                IdProduto = produto.IdProduto,
                                IdUnidade = unidade.IdUnidade,
                                Quantidade = qtde,
                                IdSolicitacaoNavigation = solicitacao
                            };
                            solicitacao.ItensSolicitacao.Add(item);
                        }
                    }

                    dbs.SolicitacaoTransporte.Add(solicitacao);

                    dbs.SaveChanges();
                    trans.Commit();

                    return solicitacao;
                }
            }
        }
    }
}



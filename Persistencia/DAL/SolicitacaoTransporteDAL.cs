using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Persistencia.DTO;
using Persistencia.Entities;
using System.ComponentModel.DataAnnotations;

namespace Persistencia.DAL
{
    #region Classes utilizadas para recebimento de nova coleta
    public class EnderecoColetaDTO
    {
        [Key]
        public long ID { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
    }

    public class InformacoesItemsDTO
    {
        [Key]
        public long ID { get; set; }
        public string Produto { get; set; }
        public string Unidade { get; set; }
        public string Quantidade { get; set; }
    }

    public class InformacoesColetaDTO
    {
        [Key]
        public long ID { get; set; }
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

        public List<InformacoesColetaDTO> ObterSolicitacoesTransporte()
        {
            List<InformacoesColetaDTO> listaIformacoesColeta = new List<InformacoesColetaDTO>();

            List<SolicitacaoTransporte> solicitacoes = new SolicitacaoTransporteDAL().Listar().ToList();
            foreach (SolicitacaoTransporte solicitacao in solicitacoes)
            {
                var sol = ObterSolicitacaoTransporte(solicitacao.IdSolicitacao);
                if (sol != null)
                    listaIformacoesColeta.Add(sol);
            }

            return listaIformacoesColeta;
        }

        public InformacoesColetaDTO ObterSolicitacaoTransporte(long _id)
        {
            InformacoesColetaDTO informacoesColeta = new InformacoesColetaDTO();
            SolicitacaoTransporte solicitacao = new SolicitacaoTransporteDAL().Obter(_id);

            if (solicitacao == null)
                return null;

            informacoesColeta.ID = solicitacao.IdSolicitacao;
            informacoesColeta.CNPJCliente = new PessoaJuridicaDAL().Obter(new ClienteDAL().Obter(solicitacao.IdCliente).IdPessoa).Cnpj.ToString();
            informacoesColeta.CNPJDestinatario = new PessoaJuridicaDAL().Obter(solicitacao.IdDestinatario).Cnpj.ToString();
            if (solicitacao.IdTransportador != null)
            {
                Fornecedor fornecedor = new FornecedorDAL().Obter(solicitacao.IdTransportador);
                if (fornecedor != null)
                    informacoesColeta.CNPJTransportador = new PessoaJuridicaDAL().Obter(fornecedor.IdPessoa).Cnpj.ToString();
            }
            informacoesColeta.Observacoes = solicitacao.Observacoes;


            EnderecoColetaDTO enderecoColetaDTO = new EnderecoColetaDTO();
            Endereco enderecoColeta = new EnderecoDAL().Obter(solicitacao.IdLocalColeta);
            enderecoColetaDTO.ID = enderecoColeta.IdEndereco;
            enderecoColetaDTO.Logradouro = enderecoColeta.Logradouro;
            enderecoColetaDTO.Numero = enderecoColeta.Numero;
            Bairro bairro = new DALControl<Bairro>().Obter(enderecoColeta.IdBairro);
            enderecoColetaDTO.Bairro = bairro.Nome;
            Municipio municipio = new DALControl<Municipio>().Obter(bairro.IdMunicipio);
            enderecoColetaDTO.Municipio = municipio.Nome;
            Estado uf = new DALControl<Estado>().Obter(municipio.IdEstado);
            enderecoColetaDTO.UF = uf.Sigla;
            informacoesColeta.LocalColeta = enderecoColetaDTO;

            EnderecoColetaDTO enderecoEntregaDTO = new EnderecoColetaDTO();
            Endereco enderecoEntrega = new EnderecoDAL().Obter(solicitacao.IdLocalEntrega);
            enderecoEntregaDTO.ID = enderecoEntrega.IdEndereco;
            enderecoEntregaDTO.Logradouro = enderecoEntrega.Logradouro;
            enderecoEntregaDTO.Numero = enderecoEntrega.Numero;
            Bairro bairroEnt = new DALControl<Bairro>().Obter(enderecoEntrega.IdBairro);
            enderecoEntregaDTO.Bairro = bairroEnt.Nome;
            Municipio municipioEnt = new DALControl<Municipio>().Obter(bairroEnt.IdMunicipio);
            enderecoEntregaDTO.Municipio = municipioEnt.Nome;
            Estado ufEnt = new DALControl<Estado>().Obter(municipioEnt.IdEstado);
            enderecoEntregaDTO.UF = ufEnt.Sigla;
            informacoesColeta.LocalEntrega = enderecoEntregaDTO;

            informacoesColeta.Itens = new ItensSolicitacaoDAL().GetInformacoesItensSolicitacao(solicitacao.IdSolicitacao);

            return informacoesColeta;
        }

        public void EditarSolicitacaoTransporte(InformacoesColetaDTO _infoColeta)
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
                    SolicitacaoTransporte solicitacao = dbs.SolicitacaoTransporte.Find(_infoColeta.ID);
                    solicitacao.IdCliente = cliente.IdPessoa;
                    solicitacao.IdLocalColeta = enderecoColeta.IdEndereco;
                    solicitacao.IdDestinatario = destinatario.IdPessoa;
                    solicitacao.IdLocalEntrega = enderecoEntrega.IdEndereco;
                    solicitacao.IdTransportador = null;
                    solicitacao.Observacoes = _infoColeta.Observacoes;

                    // Remove todos os itenr atuais
                    //solicitacao.ItensSolicitacao.Clear();

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

                            // Busca o item atual
                            //ItensSolicitacao item = solicitacao.ItensSolicitacao.Where<ItensSolicitacao>(i => i.IdItemSolicitacao == infoItemDTO.ID).First();
                            ItensSolicitacao item = dbs.ItensSolicitacao.Find(infoItemDTO.ID);
                            // Altera os valores do item atual
                            item.IdProduto = produto.IdProduto;
                            item.IdUnidade = unidade.IdUnidade;
                            item.Quantidade = qtde;
                            item.IdSolicitacaoNavigation = solicitacao;
                        }
                    }

                    //dbs.SolicitacaoTransporte.Attach(solicitacao);
                    dbs.SaveChanges();
                    trans.Commit();
                }
            }
        }

        public void RemoverSolicitacaoTransporte(long _idSolicitacao)
        {
            using (var dbs = new dabbawalaContext())
            {

                SolicitacaoTransporte solicitacao = dbs.SolicitacaoTransporte.Find(_idSolicitacao);
                List<ItensSolicitacao> items = dbs.ItensSolicitacao.Where(i => i.IdSolicitacao == solicitacao.IdSolicitacao).ToList();

                List<Expedicao> expedicoes = dbs.Expedicao.Where(e => e.IdSolicitacao == solicitacao.IdSolicitacao).ToList();

                using (var trans = dbs.Database.BeginTransaction())
                {
                    //solicitacao.ItensSolicitacao = null;
                    dbs.RemoveRange(items);
                    dbs.RemoveRange(expedicoes);
                    dbs.Remove(solicitacao);
                    dbs.SaveChanges();
                    trans.Commit();
                }
            }
        }
    }
}



using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistencia.Entities
{
    public partial class dabbawalaContext : DbContext
    {
        public dabbawalaContext()
        {
        }

        public dabbawalaContext(DbContextOptions<dabbawalaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bairro> Bairro { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Embalagem> Embalagem { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<EnderecoEletronico> EnderecoEletronico { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Expedicao> Expedicao { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<ItensSolicitacao> ItensSolicitacao { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<PessoaJuridica> PessoaJuridica { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<SolicitacaoTransporte> SolicitacaoTransporte { get; set; }
        public virtual DbSet<TipoEnderecoEletronico> TipoEnderecoEletronico { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=dabbawala;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bairro>(entity =>
            {
                entity.HasKey(e => e.IdBairro);

                entity.ToTable("BAIRRO");

                entity.HasIndex(e => e.IdMunicipio)
                    .HasName("IXFK_BAIRRO_MUNICIPIO");

                entity.Property(e => e.IdBairro).HasColumnName("id_bairro");

                entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Bairro)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_BAIRRO_MUNICIPIO");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdPessoa);

                entity.ToTable("CLIENTE");

                entity.HasIndex(e => e.IdPessoa)
                    .HasName("IXFK_CLIENTE_PESSOA_JURIDICA");

                entity.Property(e => e.IdPessoa)
                    .HasColumnName("id_pessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bloqueado).HasColumnName("bloqueado");

                entity.Property(e => e.FaturaOrigem).HasColumnName("fatura_origem");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.IdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE_PESSOA_JURIDICA");
            });

            modelBuilder.Entity<Embalagem>(entity =>
            {
                entity.HasKey(e => e.IdEmbalagem);

                entity.ToTable("EMBALAGEM");

                entity.Property(e => e.IdEmbalagem).HasColumnName("id_embalagem");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmpilhamentoMaximo).HasColumnName("empilhamento_maximo");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.IdEndereco);

                entity.ToTable("ENDERECO");

                entity.HasIndex(e => e.IdBairro)
                    .HasName("IXFK_ENDERECO_BAIRRO");

                entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");

                entity.Property(e => e.Cep)
                    .HasColumnName("cep")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IdBairro).HasColumnName("id_bairro");

                entity.Property(e => e.Logradouro)
                    .HasColumnName("logradouro")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBairroNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.IdBairro)
                    .HasConstraintName("FK_ENDERECO_BAIRRO");
            });

            modelBuilder.Entity<EnderecoEletronico>(entity =>
            {
                entity.HasKey(e => e.IdEnderecoEletronico);

                entity.ToTable("ENDERECO_ELETRONICO");

                entity.HasIndex(e => e.IdTipendEletronico)
                    .HasName("IXFK_ENDERECO_ELETRONICO_TIPO_ENDERECO_ELETRONICO");

                entity.Property(e => e.IdEnderecoEletronico).HasColumnName("id_endereco_eletronico");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipendEletronico).HasColumnName("id_tipend_eletronico");

                entity.HasOne(d => d.IdTipendEletronicoNavigation)
                    .WithMany(p => p.EnderecoEletronico)
                    .HasForeignKey(d => d.IdTipendEletronico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ENDERECO_ELETRONICO_TIPO_ENDERECO_ELETRONICO");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("ESTADO");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sigla)
                    .HasColumnName("sigla")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Expedicao>(entity =>
            {
                entity.HasKey(e => e.IdExpedicao);

                entity.ToTable("EXPEDICAO");

                entity.HasIndex(e => e.IdSolicitacao)
                    .HasName("IXFK_EXPEDICAO_SOLICITACAO");

                entity.Property(e => e.IdExpedicao).HasColumnName("id_expedicao");

                entity.Property(e => e.IdSolicitacao).HasColumnName("id_solicitacao");

                entity.Property(e => e.PrevisaoEntrega)
                    .HasColumnName("previsao_entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSolicitacaoNavigation)
                    .WithMany(p => p.Expedicao)
                    .HasForeignKey(d => d.IdSolicitacao)
                    .HasConstraintName("FK_EXPEDICAO_SOLICITACAO");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.IdPessoa);

                entity.ToTable("FORNECEDOR");

                entity.HasIndex(e => e.IdPessoa)
                    .HasName("IXFK_FORNECEDOR_PESSOA_JURIDICA");

                entity.Property(e => e.IdPessoa)
                    .HasColumnName("id_pessoa")
                    .ValueGeneratedNever();

                entity.Property(e => e.CargaPerecível).HasColumnName("carga_perecível");

                entity.Property(e => e.CargaPerigosa).HasColumnName("carga_perigosa");

                entity.Property(e => e.Interestadual).HasColumnName("interestadual");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithOne(p => p.Fornecedor)
                    .HasForeignKey<Fornecedor>(d => d.IdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FORNECEDOR_PESSOA_JURIDICA");
            });

            modelBuilder.Entity<ItensSolicitacao>(entity =>
            {
                entity.HasKey(e => e.IdItemSolicitacao);

                entity.ToTable("ITENS_SOLICITACAO");

                entity.HasIndex(e => e.IdProduto)
                    .HasName("IXFK_ITENS_SOLICITACAO_PRODUTO");

                entity.HasIndex(e => e.IdSolicitacao)
                    .HasName("IXFK_ITENS_SOLICITACAO_SOLICITACAO_TRANSPORTE");

                entity.HasIndex(e => e.IdUnidade)
                    .HasName("IXFK_ITENS_SOLICITACAO_UNIDADE");

                entity.HasIndex(e => new { e.IdSolicitacao, e.IdProduto })
                    .HasName("UK_SOLICITACAO_PRODUTO")
                    .IsUnique();

                entity.Property(e => e.IdItemSolicitacao).HasColumnName("id_item_solicitacao");

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.IdSolicitacao).HasColumnName("id_solicitacao");

                entity.Property(e => e.IdUnidade).HasColumnName("id_unidade");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.ItensSolicitacao)
                    .HasForeignKey(d => d.IdProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITENS_SOLICITACAO_PRODUTO");

                entity.HasOne(d => d.IdSolicitacaoNavigation)
                    .WithMany(p => p.ItensSolicitacao)
                    .HasForeignKey(d => d.IdSolicitacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ITENS_SOLICITACAO_SOLICITACAO_TRANSPORTE");

                entity.HasOne(d => d.IdUnidadeNavigation)
                    .WithMany(p => p.ItensSolicitacao)
                    .HasForeignKey(d => d.IdUnidade)
                    .HasConstraintName("FK_ITENS_SOLICITACAO_UNIDADE");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.ToTable("MUNICIPIO");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("IXFK_MUNICIPIO_ESTADO");

                entity.Property(e => e.IdMunicipio).HasColumnName("id_municipio");

                entity.Property(e => e.CodigoIbge)
                    .HasColumnName("codigo_ibge")
                    .HasColumnType("decimal(10, 0)");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FK_MUNICIPIO_ESTADO");
            });

            modelBuilder.Entity<PessoaJuridica>(entity =>
            {
                entity.HasKey(e => e.IdPessoa);

                entity.ToTable("PESSOA_JURIDICA");

                entity.HasIndex(e => e.IdEndereco)
                    .HasName("IXFK_PESSOA_JURIDICA_ENDERECO");

                entity.HasIndex(e => e.IdEnderecoEletronico)
                    .HasName("IXFK_PESSOA_JURIDICA_ENDERECO_ELETRONICO");

                entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");

                entity.Property(e => e.Cnpj)
                    .HasColumnName("cnpj")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.IdEndereco).HasColumnName("id_endereco");

                entity.Property(e => e.IdEnderecoEletronico).HasColumnName("id_endereco_eletronico");

                entity.Property(e => e.NomeFantasia)
                    .HasColumnName("nome_fantasia")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RazaoSocial)
                    .IsRequired()
                    .HasColumnName("razao_social")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnderecoNavigation)
                    .WithMany(p => p.PessoaJuridica)
                    .HasForeignKey(d => d.IdEndereco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOA_JURIDICA_ENDERECO");

                entity.HasOne(d => d.IdEnderecoEletronicoNavigation)
                    .WithMany(p => p.PessoaJuridica)
                    .HasForeignKey(d => d.IdEnderecoEletronico)
                    .HasConstraintName("FK_PESSOA_JURIDICA_ENDERECO_ELETRONICO");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto);

                entity.ToTable("PRODUTO");

                entity.HasIndex(e => e.IdEmbalagem)
                    .HasName("IXFK_PRODUTO_EMBALAGEM");

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.CodigoNcm)
                    .HasColumnName("codigo_ncm")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdEmbalagem).HasColumnName("id_embalagem");

                entity.Property(e => e.Perecivel).HasColumnName("perecivel");

                entity.Property(e => e.Perigoso).HasColumnName("perigoso");

                entity.Property(e => e.QtdePorEmbalagem).HasColumnName("qtde_por_embalagem");

                entity.HasOne(d => d.IdEmbalagemNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdEmbalagem)
                    .HasConstraintName("FK_PRODUTO_EMBALAGEM");
            });

            modelBuilder.Entity<SolicitacaoTransporte>(entity =>
            {
                entity.HasKey(e => e.IdSolicitacao);

                entity.ToTable("SOLICITACAO_TRANSPORTE");

                entity.HasIndex(e => e.IdCliente)
                    .HasName("IXFK_SOLICITACAO_TRANSPORTE_CLIENTE");

                entity.HasIndex(e => e.IdDestinatario)
                    .HasName("IXFK_SOLICITACAO_TRANSPORTE_PESSOA_JURIDICA");

                entity.HasIndex(e => e.IdLocalColeta)
                    .HasName("IXFK_SOLICITACAO_TRANSPORTE_ENDERECO");

                entity.HasIndex(e => e.IdLocalEntrega)
                    .HasName("IXFK_SOLICITACAO_TRANSPORTE_LOCAL_ENTREGA");

                entity.HasIndex(e => e.IdTransportador)
                    .HasName("IXFK_SOLICITACAO_TRANSPORTE_FORNECEDOR");

                entity.Property(e => e.IdSolicitacao).HasColumnName("id_solicitacao");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdDestinatario).HasColumnName("id_destinatario");

                entity.Property(e => e.IdLocalColeta).HasColumnName("id_local_coleta");

                entity.Property(e => e.IdLocalEntrega).HasColumnName("id_local_entrega");

                entity.Property(e => e.IdTransportador).HasColumnName("id_transportador");

                entity.Property(e => e.Observacoes)
                    .HasColumnName("observacoes")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.SolicitacaoTransporte)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOLICITACAO_TRANSPORTE_CLIENTE");

                entity.HasOne(d => d.IdDestinatarioNavigation)
                    .WithMany(p => p.SolicitacaoTransporte)
                    .HasForeignKey(d => d.IdDestinatario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOLICITACAO_TRANSPORTE_PESSOA_JURIDICA");

                entity.HasOne(d => d.IdLocalColetaNavigation)
                    .WithMany(p => p.SolicitacaoTransporteIdLocalColetaNavigation)
                    .HasForeignKey(d => d.IdLocalColeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOLICITACAO_TRANSPORTE_LOCAL_COLETA");

                entity.HasOne(d => d.IdLocalEntregaNavigation)
                    .WithMany(p => p.SolicitacaoTransporteIdLocalEntregaNavigation)
                    .HasForeignKey(d => d.IdLocalEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOLICITACAO_TRANSPORTE_LOCAL_ENTREGA");

                entity.HasOne(d => d.IdTransportadorNavigation)
                    .WithMany(p => p.SolicitacaoTransporte)
                    .HasForeignKey(d => d.IdTransportador)
                    .HasConstraintName("FK_SOLICITACAO_TRANSPORTE_FORNECEDOR");
            });

            modelBuilder.Entity<TipoEnderecoEletronico>(entity =>
            {
                entity.HasKey(e => e.IdTipendEletronico);

                entity.ToTable("TIPO_ENDERECO_ELETRONICO");

                entity.Property(e => e.IdTipendEletronico).HasColumnName("id_tipend_eletronico");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.HasKey(e => e.IdUnidade);

                entity.ToTable("UNIDADE");

                entity.Property(e => e.IdUnidade).HasColumnName("id_unidade");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sigla)
                    .HasColumnName("sigla")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });
        }
    }
}

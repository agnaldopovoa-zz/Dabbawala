using System;
using System.Collections.Generic;
using System.Text;
using Persistencia.Entities;
using Newtonsoft.Json;

namespace Persistencia.DAL
{
    public class EntidadesDAL 
    {
        public string GetEntityByName(dabbawalaContext _context, string _entidade)
        {
            if (_entidade.Trim().ToUpper().Equals("ESTADO")) return JsonConvert.SerializeObject(new DALControl<Estado>().Listar());
            if (_entidade.Trim().ToUpper().Equals("MUNICIPIO")) return JsonConvert.SerializeObject(new DALControl<Municipio>().Listar());
            if (_entidade.Trim().ToUpper().Equals("BAIRRO")) return JsonConvert.SerializeObject(new DALControl<Bairro>().Listar());
            if (_entidade.Trim().ToUpper().Equals("ENDERECO")) return JsonConvert.SerializeObject(new DALControl<Endereco>().Listar());
            if (_entidade.Trim().ToUpper().Equals("TIPO_ENDERECO_ELETRONICO")) return JsonConvert.SerializeObject(new DALControl<TipoEnderecoEletronico>().Listar());
            if (_entidade.Trim().ToUpper().Equals("ENDERECO_ELETRONICO")) return JsonConvert.SerializeObject(new DALControl<EnderecoEletronico>().Listar());
            if (_entidade.Trim().ToUpper().Equals("PESSOA_JURIDICA")) return JsonConvert.SerializeObject(new DALControl<PessoaJuridica>().Listar());
            if (_entidade.Trim().ToUpper().Equals("FORNECEDOR")) return JsonConvert.SerializeObject(new DALControl<Fornecedor>().Listar());
            if (_entidade.Trim().ToUpper().Equals("CLIENTE")) return JsonConvert.SerializeObject(new DALControl<Cliente>().Listar());
            if (_entidade.Trim().ToUpper().Equals("SOLICITACAO_TRANSPORTE")) return JsonConvert.SerializeObject(new DALControl<SolicitacaoTransporte>().Listar());
            if (_entidade.Trim().ToUpper().Equals("EMBALAGEM")) return JsonConvert.SerializeObject(new DALControl<Embalagem>().Listar());
            if (_entidade.Trim().ToUpper().Equals("UNIDADE")) return JsonConvert.SerializeObject(new DALControl<Unidade>().Listar());
            if (_entidade.Trim().ToUpper().Equals("PRODUTO")) return JsonConvert.SerializeObject(new DALControl<Produto>().Listar());
            if (_entidade.Trim().ToUpper().Equals("ITENS_SOLICITACAO")) return JsonConvert.SerializeObject(new DALControl<ItensSolicitacao>().Listar());
            if (_entidade.Trim().ToUpper().Equals("EXPEDICAO")) return JsonConvert.SerializeObject(new DALControl<Expedicao>().Listar());

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Entities;

namespace Persistencia.DAL
{
    public class ExpedicaoDAL : DALControl<Expedicao>
    {
        public Expedicao CriarNovaExpedicao(long _idSolicitacao)
        {
            using (var dbs = new dabbawalaContext())
            {
                //  Busca a solicita��o
                SolicitacaoTransporte solicitacao = new SolicitacaoTransporteDAL().Obter(_idSolicitacao);
                if (solicitacao == null)
                    throw new Exception("N�o foi encontrado solicita��o de transporte com ID [" + _idSolicitacao.ToString() + "]");

                using (var trans = dbs.Database.BeginTransaction())
                {
                    // Gera uma data de previs�o (aleat�ria)
                    DateTime dataPrevisao = DateTime.Now;
                    Random random = new Random();
                    dataPrevisao = dataPrevisao.AddDays(random.Next(0,3));
                    dataPrevisao = dataPrevisao.AddHours(random.Next(1, 23));
                    dataPrevisao = dataPrevisao.AddMinutes(random.Next(1, 59));
                    dataPrevisao = dataPrevisao.AddSeconds(random.Next(1, 59));

                    // Cria a nova expedi��o
                    var expedicao = new Expedicao
                    {
                        IdSolicitacao = solicitacao.IdSolicitacao,
                        PrevisaoEntrega = dataPrevisao,
                        Status = "1"
                    };

                    // Salva e commita as altera��es
                    dbs.SaveChanges();
                    trans.Commit();

                    return expedicao;
                }
            }
        }
    }
}

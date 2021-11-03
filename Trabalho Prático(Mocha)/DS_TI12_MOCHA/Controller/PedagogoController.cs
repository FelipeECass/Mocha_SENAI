using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_TI12_MOCHA.View;
using DS_TI12_MOCHA.Model;
using System.Data;

namespace DS_TI12_MOCHA.Controller
{
    class PedagogoController
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V= new TurmaView();
        AcessoMySQL Banco = new AcessoMySQL();
        string Sql = "";

        public void InserirNovaTurma(TurmaView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select uni_id from db_escolagrupo2.unidade where uni_nome ='{0}'", Parametros.Tur_unidade);
            Sql = String.Format("insert into db_escolagrupo2.turma (tur_nome, tur_unidade) value ('{0}', {1})", Parametros.Tur_nome, Banco.BuscarValorSelect(Sql));

            Banco.Inserir(Sql);
        }

        public void MandarAviso(AvisosView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", Parametros.Avi_mando);
            Sql = String.Format("select aca_nome from academico where aca_id = '{0}'", Banco.BuscarValorSelect(Sql));
            Sql = String.Format("insert into db_escolagrupo2.aviso (avi_texto, avi_mando, avi_recebeu) value ('{0}', '{1}', '{2}')", Parametros.Avi_texto, Parametros.Avi_mando, Parametros.Avi_recebeu);

            Banco.Inserir(Sql);
        }

        public DataTable ReceberAviso(string nick)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from db_escolagrupo2.usuario where usu_nick='{0}'", nick);
            Sql = String.Format("select aca_nome from db_escolagrupo2.academico where aca_id='{0}'", Banco.BuscarValorSelect(Sql));
            Sql = String.Format("select avi_texto as Mensagem,avi_mando as Remetente from db_escolagrupo2.aviso where avi_recebeu='Pedagogo(a)' or avi_recebeu='{0}'", Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public DataTable FaltasProfessor()
        {
            Banco.ConectarMySql();

            Sql = String.Format("select aca_nome as 'Nome', fal_data as 'Data' from falta inner join academico on academico.aca_id=falta.fk_aca_id where academico.aca_tipo='Professor(a)';");

            return Banco.BuscarTabela(Sql);
        }
    }
}

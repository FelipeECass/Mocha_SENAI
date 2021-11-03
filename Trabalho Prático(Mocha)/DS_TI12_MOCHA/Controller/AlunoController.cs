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
    class AlunoController
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V = new TurmaView();
        AcessoMySQL Banco = new AcessoMySQL();
        string Sql = "";

        public DataTable ReceberAviso(string nick)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from db_escolagrupo2.usuario where usu_nick='{0}'",nick);
            Sql = String.Format("select aca_nome from db_escolagrupo2.academico where aca_id='{0}'",Banco.BuscarValorSelect(Sql));
            Sql = String.Format("select avi_texto as Mensagem,avi_mando as Remetente from db_escolagrupo2.aviso where avi_recebeu='Aluno(a)' or avi_recebeu='{0}'", Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public DataTable VisualizarNota(string nick)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", nick);
            Sql = String.Format("select materia.mat_nome as 'Materia', alm_nota1 as 'Nota1', alm_nota2 as 'Nota2', alm_simulado as 'Simulado', alm_trab1 as 'Trabalho', alm_avalia as 'Ativ. Avaliativa' from aluno_materia inner join materia on aluno_materia.alm_materia=materia.mat_id where alm_academico='{0}';",Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public DataTable VisualizarFaltas(string nick)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", nick);
            Sql = String.Format("select fal_data as Data from falta where fk_aca_id='{0}';", Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public int TotFaltas(string nick)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", nick);
            Sql = String.Format("select count(fal_data) from falta where fk_aca_id='{0}';", Banco.BuscarValorSelect(Sql));

            return Convert.ToInt32(Banco.BuscarValorSelect(Sql));
        }

        public DataTable ChamarMateria()
        {
            Banco.ConectarMySql();

            return Banco.BuscarTabela("select * from db_escolagrupo2.materia");
        }

        public void MandarAviso(AvisosView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", Parametros.Avi_mando);
            Sql = String.Format("select aca_nome from academico where aca_id = '{0}'", Banco.BuscarValorSelect(Sql));
            Sql = String.Format("insert into db_escolagrupo2.aviso (avi_texto, avi_mando, avi_recebeu) value ('{0}', '{1}', '{2}')", Parametros.Avi_texto, Banco.BuscarValorSelect(Sql), Parametros.Avi_recebeu);

            Banco.Inserir(Sql);
        }
    }


}

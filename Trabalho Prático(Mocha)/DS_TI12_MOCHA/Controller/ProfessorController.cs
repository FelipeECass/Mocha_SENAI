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
    class ProfessorController
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V = new TurmaView();
        AcessoMySQL Banco = new AcessoMySQL();
        Aluno_MateriaView AlMat_V = new Aluno_MateriaView();
        string Sql = "";

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
            Sql = String.Format("select avi_texto as Mensagem,avi_mando as Remetente from db_escolagrupo2.aviso where avi_recebeu='Professor(a)' or avi_recebeu='{0}'", Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public DataTable VisualizarTurma(string nick, string turma)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", nick);
            Sql = String.Format("select pfm_materia from professor_materia where pfm_academico='{0}'",Banco.BuscarValorSelect(Sql));
            string Sql2 = String.Format("select tur_matricula from turma where tur_nome='{0}'", turma);
            AlMat_V.Alm_materia = Convert.ToString(Banco.BuscarValorSelect(Sql));
            Sql = String.Format("select aca_nome as 'Nome', alm_nota1 as 'Nota1', alm_nota2 as 'Nota2', alm_simulado as 'Simulado', alm_trab1 as 'Trabalho', alm_avalia as 'Ativ. Avaliativa' from aluno_materia inner join academico on aluno_materia.alm_academico=academico.aca_id where aluno_materia.alm_turma='{0}' and aluno_materia.alm_materia='{1}'", Banco.BuscarValorSelect(Sql2), Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public void UpdateNotasTurma(Aluno_MateriaView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from usuario where usu_nick = '{0}'", Parametros.Alm_materia);
            Sql = String.Format("select pfm_materia from professor_materia where pfm_academico='{0}'", Banco.BuscarValorSelect(Sql));
            Parametros.Alm_materia = Convert.ToString(Banco.BuscarValorSelect(Sql));
            Sql = String.Format("select tur_matricula from turma where tur_nome='{0}'", Parametros.Alm_turma);
            Parametros.Alm_turma = Convert.ToString(Banco.BuscarValorSelect(Sql));
            Sql = String.Format("update aluno_materia set alm_nota1 = '{0}', alm_nota2 = '{1}', alm_simulado = '{2}', alm_trab1 = '{3}', alm_avalia = '{4}' where(select aca_id from academico where aca_nome = '{5}') and alm_materia = '{6}' and alm_turma = '{7}'", Parametros.Alm_nota1, Parametros.Alm_nota2, Parametros.Alm_simulado, Parametros.Alm_trab1, Parametros.Alm_avalia, Parametros.Alm_academico,Parametros.Alm_materia,Parametros.Alm_turma);

            Banco.Inserir(Sql);
        }

        public DataTable ChamarTurmas()
        {
            Banco.ConectarMySql();

            return Banco.BuscarTabela("select * from db_escolagrupo2.turma");
        }

        public DataTable ListadeFaltas(string turma)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select tur_matricula from turma where tur_nome='{0}'",turma);
            Sql = String.Format("select aca_nome as Nome from aluno_materia inner join academico on alm_academico=aca_id where aluno_materia.alm_turma='{0}'",Banco.BuscarValorSelect(Sql));

            return Banco.BuscarTabela(Sql);
        }

        public void InserirFaltas(FaltaView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select aca_id from academico where aca_nome='{0}'",Parametros.Fal_id);
            Sql = String.Format("insert into falta(fal_data, fk_aca_id) value ('{0}','{1}')",Parametros.Fal_data,Banco.BuscarValorSelect(Sql));

            Banco.Inserir(Sql);
        }
    }
}

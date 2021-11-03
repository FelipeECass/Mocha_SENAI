using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_TI12_MOCHA.Model;
using DS_TI12_MOCHA.View;
using System.Data;

namespace DS_TI12_MOCHA.Controller
{
    class MatProfessorController
    {
        AcessoMySQL Banco = new AcessoMySQL();
        MatProfessorView MatProf_V = new MatProfessorView();
        string Sql;
        public DataTable ChamarMateria()
        {
            Banco.ConectarMySql();

            return Banco.BuscarTabela("select * from db_escolagrupo2.materia");
        }

        public void InserirNovaMateriaProfessor(MatProfessorView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select max(aca_id) from db_escolagrupo2.academico;");

            Sql = String.Format("insert into db_escolagrupo2.professor_materia (pfm_academico, pfm_materia) value ({0},{1});", Banco.BuscarValorSelect(Sql), Parametros.Mat_nome);

            Banco.Inserir(Sql);
        }
    }
}

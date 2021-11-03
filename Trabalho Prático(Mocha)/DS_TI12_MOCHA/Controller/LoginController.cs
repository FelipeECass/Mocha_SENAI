using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_TI12_MOCHA.View;
using DS_TI12_MOCHA.Model;

namespace DS_TI12_MOCHA.Controller
{
    class LoginController
    {
        string Sql;
        AcessoMySQL Banco = new AcessoMySQL();

        public bool ExecutaLogin(LoginView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select usu_nick from db_escolagrupo2.usuario where binary usu_nick ='{0}' and binary usu_senha ='{1}'", Parametros.Usu_nick, Parametros.Usu_senha);

            return Banco.ConferirUsurio(Sql);
        }

        public string ConferirTipoUsuario(LoginView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from db_escolagrupo2.usuario where binary usu_nick ='{0}' and binary usu_senha ='{1}'", Parametros.Usu_nick, Parametros.Usu_senha);
            Sql = String.Format("select aca_tipo from db_escolagrupo2.academico where aca_id = '{0}'",Banco.BuscarValorSelect(Sql));

            return Convert.ToString(Banco.BuscarValorSelect(Sql));
        }
    }
}

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
    class CadastroController
    {
        AcademicoView Cadastro = new AcademicoView();
        AcessoMySQL Banco = new AcessoMySQL();
        LoginView Login = new LoginView();
        string Sql = "";
        int id;

        public void InserirNovoAcademico(AcademicoView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("insert into db_escolagrupo2.academico (aca_nome, aca_cpf, aca_datanasc, aca_sexo, aca_endereco, aca_bairro, aca_numero, aca_complemento, aca_telefone, aca_celular, aca_cep, aca_email, aca_tipo, aca_obs, aca_cidade) value ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}', '{14}');", Parametros.Aca_nome, Parametros.Aca_cpf, Parametros.Aca_datanasc, Parametros.Aca_sexo, Parametros.Aca_endereco, Parametros.Aca_bairro, Parametros.Aca_numero, Parametros.Aca_complemento, Parametros.Aca_telefone, Parametros.Aca_celular, Parametros.Aca_cep, Parametros.Aca_email, Parametros.Aca_tipo, Parametros.Aca_obs, Parametros.Aca_cidade);

            Banco.Inserir(Sql);
        }

        public int BuscarIdCliente()
        {
            Banco.ConectarMySql();

            Sql = String.Format("select max(aca_id) from db_escolagrupo2.academico;");

            id = Convert.ToInt32(Banco.BuscarValorSelect(Sql));

            Login.Fk_aca_id = id;
            return id;
        }

        public void InserirNovoUsuario(LoginView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("insert into db_escolagrupo2.usuario (usu_nick, usu_senha, fk_aca_id) value ('{0}', '{1}', {2});", Parametros.Usu_nick, Parametros.Usu_senha, Login.Fk_aca_id);

            Banco.Inserir(Sql);
        }

        public int BuscarIdUsuario()
        {
            Banco.ConectarMySql();

            Sql = String.Format("select usu_nick from db_escolagrupo2.usuario order by fk_aca_id desc limit 1;");

            id = Convert.ToInt32(Banco.BuscarValorSelect(Sql));

            return id;
        }

        public string ConferirTipoUsuario(LoginView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select fk_aca_id from db_escolagrupo2.usuario where binary usu_nick ='{0}' and binary usu_senha ='{1}'", Parametros.Usu_nick, Parametros.Usu_senha);
            Sql = String.Format("select aca_tipo from db_escolagrupo2.academico where aca_id = '{0}'", Banco.BuscarValorSelect(Sql));

            return Convert.ToString(Banco.BuscarValorSelect(Sql));
        }

        public DataTable ChamarEstado()
        {
            Banco.ConectarMySql();

            return Banco.BuscarTabela("select * from db_escolagrupo2.estado");
        }

        public string BuscarCidade(AcademicoView Parametros)
        {
            Banco.ConectarMySql();

            Sql = String.Format("select est_id from db_escolagrupo2.estado where est_uf='{0}'", Parametros.Aca_estado);
            Sql = String.Format("select cid_id from db_escolagrupo2.cidade where cid_nome='{0}' and fk_est_id='{1}'", Parametros.Aca_cidade, Banco.BuscarValorSelect(Sql));

            return Convert.ToString(Banco.BuscarValorSelect(Sql));
        }
    }
}

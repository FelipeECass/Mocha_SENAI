using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DS_TI12_MOCHA.Model
{
    class AcessoMySQL
    {
        private MySqlConnection MyConect;
        MySqlCommand MyCommand;

        private string SERVER = "localhost";
        private string PORT = "3306";
        private string DATABASE = "db_escolagrupo2";
        private string UID = "root";
        private string PASSWORD = "";
        private string SSLMODE = "none";

        public void ConectarMySql()
        {
            if (MyConect != null)
            {
                MyConect.Close();
            }

            string MyBanco = String.Format("SERVER={0}; PORT={1}; DATABASE={2}; UID={3}; PASSWORD={4}; SSLMODE={5};", SERVER, PORT, DATABASE, UID, PASSWORD, SSLMODE);

            try
            {
                MyConect = new MySqlConnection(MyBanco);
                MyConect.Open();
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Inserir(string Sql)
        {
            try
            {
                MyCommand = new MySqlCommand(Sql, MyConect);
                MyCommand.ExecuteNonQuery();
                MyConect.Close();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object BuscarValorSelect(string Sql)
        {
            try
            {
                MyCommand = new MySqlCommand(Sql, MyConect);

                return MyCommand.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConferirUsurio(string Sql)
        {
            try
            { 
            MyCommand = new MySqlCommand(Sql,MyConect);

            MySqlDataReader mySqlDataReader = MyCommand.ExecuteReader();

            if (mySqlDataReader.HasRows == true)
                return true;
            else
                return false;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable BuscarTabela(String Sql)
        {
            try
            { 
            DataTable MyTabela = new DataTable();
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter(Sql, MyConect);
            MySqlCommandBuilder MyComandBuilder = new MySqlCommandBuilder(MyAdapter);
            MyAdapter.Fill(MyTabela);
            return MyTabela;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

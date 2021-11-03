using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DS_TI12_MOCHA.Controller;
using DS_TI12_MOCHA.View;


namespace DS_TI12_MOCHA
{
    public partial class FormCadastro : Form
    {
        AcademicoView Cadastro_V = new AcademicoView();
        CadastroController Cadastro_C = new CadastroController();
        LoginView Login_V = new LoginView();

        public FormCadastro()
        {
            InitializeComponent();

            cmbBx_Estado.ValueMember = "est_id";
            cmbBx_Estado.DisplayMember = "est_uf";

            cmbBx_Estado.DataSource = Cadastro_C.ChamarEstado();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormPedagogo Fd = new FormPedagogo("");
            Fd.Closed += (s, args) => this.Close();
            Fd.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Inserir academico
            Cadastro_V.Aca_nome = txtBx_Nome.Text;
            Cadastro_V.Aca_cpf = mskdTxtBx_Cpf.Text.Replace(".", "").Replace("-", "");
            Cadastro_V.Aca_datanasc = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Cadastro_V.Aca_sexo = cmbBx_Sexo.Text;
            Cadastro_V.Aca_endereco = txtBx_Endereco.Text;
            Cadastro_V.Aca_cidade = txtBx_Cidade.Text;
            Cadastro_V.Aca_estado = cmbBx_Estado.Text;
            Cadastro_V.Aca_bairro = txtBx_Bairro.Text;
            Cadastro_V.Aca_numero = txtBx_Numero.Text;
            Cadastro_V.Aca_complemento = txtBx_Complemento.Text;
            Cadastro_V.Aca_telefone = mskdTxtBx_Tel.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ","");
            Cadastro_V.Aca_celular = mskdTxtBx_Cel.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            Cadastro_V.Aca_cep = mskdTxtBx_Cep.Text.Replace("-", "");
            Cadastro_V.Aca_email = txtBx_Email.Text;
            Cadastro_V.Aca_tipo = cmbBx_Tipo.Text;
            Cadastro_V.Aca_obs = txtBx_Obs.Text;

            Cadastro_V.Aca_cidade = Cadastro_C.BuscarCidade(Cadastro_V);


            if (Cadastro_V.Aca_nome == "" || Cadastro_V.Aca_cpf == "   .   .   " || Cadastro_V.Aca_sexo == "" || Cadastro_V.Aca_endereco == "" || Cadastro_V.Aca_bairro == "" || Cadastro_V.Aca_numero == "" || Cadastro_V.Aca_complemento == "" || Cadastro_V.Aca_telefone == "" || Cadastro_V.Aca_celular == "" || Cadastro_V.Aca_cep == "" || Cadastro_V.Aca_email == "" || Cadastro_V.Aca_tipo == "" || Cadastro_V.Aca_obs == "")
            {
                MessageBox.Show("Preencha todas as informações.");
            }
            else
            {
                //Inserir usuario
                if (DateTime.Now.Year - dateTimePicker1.Value.Year > 5 || DateTime.Now.Year - dateTimePicker1.Value.Year == 5 && DateTime.Now.Month - dateTimePicker1.Value.Month > 0 || DateTime.Now.Year - dateTimePicker1.Value.Year == 5 && DateTime.Now.Day - dateTimePicker1.Value.Day > 0)
                {
                    if (txtBx_Senha.Text == txtBx_ConSenha.Text)
                    {
                        Cadastro_C.InserirNovoAcademico(Cadastro_V);

                        int usu_id = Cadastro_C.BuscarIdCliente();
                        Login_V.Usu_nick = DateTime.Now.Year + "0" + (cmbBx_Tipo.SelectedIndex + 1) + "0" + usu_id;

                        Login_V.Usu_senha = txtBx_Senha.Text;
                        Cadastro_C.InserirNovoUsuario(Login_V);

                        MessageBox.Show("O usuário do novo academico registrado é:" + Convert.ToString(Cadastro_C.BuscarIdUsuario()));
                        if(Cadastro_C.ConferirTipoUsuario(Login_V)=="Professor(a)")
                        {
                            this.Hide();
                            FormMatProfessor Fmp = new FormMatProfessor();
                            Fmp.Closed += (s, args) => this.Close();
                            Fmp.Show();
                        }
                        else
                        {
                            this.Hide();
                            FormCadastro Fc = new FormCadastro();
                            Fc.Closed += (s, args) => this.Close();
                            Fc.Show();
                        }
                    }
                    else
                    {
                        txtBx_Senha.Text = "";
                        txtBx_ConSenha.Text = "";
                        label18.Visible = true;
                    }
                }
                else
                {
                    label21.Visible = true;
                }
            }
            notifyIcon1.Visible = true;

            this.notifyIcon1.Visible = false;
        }
    }
}
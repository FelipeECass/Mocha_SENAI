using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DS_TI12_MOCHA.Controller;
using DS_TI12_MOCHA.View;

namespace DS_TI12_MOCHA
{
    public partial class FormLogin : Form
    {
        LoginView Login_V = new LoginView();
        LoginController Login_C = new LoginController();

        public FormLogin()
        {
            InitializeComponent();
        }

        void PassarTela()
        {
            Login_V.Usu_nick = textBox1.Text;
            Login_V.Usu_senha = textBox2.Text;

            string tipo = Login_C.ConferirTipoUsuario(Login_V);

            if (Login_C.ExecutaLogin(Login_V) == true)
            {
                if (tipo == "Aluno(a)")
                {
                    this.Hide();
                    FormAluno Fa = new FormAluno(textBox1.Text);
                    Fa.Closed += (s, args) => this.Close();
                    Fa.Show();
                }
                else if (tipo == "Professor(a)")
                {
                    this.Hide();
                    FormProfessor Fp = new FormProfessor(textBox1.Text);
                    Fp.Closed += (s, args) => this.Close();
                    Fp.Show();
                }
                else if (tipo == "Pedagogo(a)")
                {
                    this.Hide();
                    FormPedagogo Fd = new FormPedagogo(textBox1.Text);
                    Fd.Closed += (s, args) => this.Close();
                    Fd.Show();
                }
            } else  MessageBox.Show("Usuário ou Senha Incorretos", "ERRO"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PassarTela();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PassarTela();
            }
        }

        private void FormLogin_MaximumSizeChanged(object sender, EventArgs e)
        {
            groupBox1.Height = 420;//altura
            groupBox1.Width = 700;//larguar
        }
    }
}

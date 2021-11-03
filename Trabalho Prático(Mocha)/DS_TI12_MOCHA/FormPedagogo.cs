using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DS_TI12_MOCHA.View;
using DS_TI12_MOCHA.Controller;

namespace DS_TI12_MOCHA
{
    public partial class FormPedagogo : Form
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V = new TurmaView();
        PedagogoController Pedagogo_C = new PedagogoController();
        public FormPedagogo(string nick)
        {
            InitializeComponent();
            textBoxUso.Text = nick;
        }

        private void segundaChamadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            dataGridView1.Visible = false;
            groupBox2.Visible = false;
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            dataGridView1.Visible = false;
        }

        private void avisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;

            dataGridView1.DataSource = Pedagogo_C.ReceberAviso(textBoxUso.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tem certeza de deseja encerrar o Portal?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja Sair do Portal de Notas?", "Log off", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Saindo do Portal");

                FormLogin Fl = new FormLogin();
                Fl.Closed += (s, args) => this.Close();
                Fl.Show();
                this.Hide();
            }
        }

        private void professoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            groupBox2.Visible = false;
            groupBox1.Visible = false;

            dataGridView1.DataSource = Pedagogo_C.FaltasProfessor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin Fl = new FormLogin();
            Fl.FormClosed += (s, args) => this.Close();
            Fl.Show();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCadastro Fc = new FormCadastro();
            Fc.FormClosed += (s, args) => this.Close();
            Fc.Show();
        }

        private void FormPedagogo_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            button3.Location = new Point(10, 10);
            button4.Location = new Point(10, 10);
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            Turma_V.Tur_nome = Convert.ToString(txtBx_nome.Text);
            Turma_V.Tur_unidade = Convert.ToString(txtBx_unidade.Text);

            Pedagogo_C.InserirNovaTurma(Turma_V);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == false && checkBox3.Checked == false)
                Aviso_V.Avi_recebeu = "Aluno(a)";
            else if (checkBox1.Checked == false && checkBox2.Checked == true && checkBox3.Checked == false)
                Aviso_V.Avi_recebeu = "Professor(a)";
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == true)
                Aviso_V.Avi_recebeu = "Pedagogo(a)";
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && textBoxid.Text != "")
                Aviso_V.Avi_recebeu = textBoxid.Text;
            else
            {
                MessageBox.Show("Selecione somente um destinatário");
                Aviso_V.Avi_recebeu = "Ninguém";
            }
            if(Aviso_V.Avi_recebeu!="Ninguém")
            { 
            Aviso_V.Avi_texto = textBox1.Text;
            Aviso_V.Avi_mando = textBoxUso.Text;

            Pedagogo_C.MandarAviso(Aviso_V);
            MessageBox.Show("Mensagem enviada com sucesso!");
            }
        }
    }
}

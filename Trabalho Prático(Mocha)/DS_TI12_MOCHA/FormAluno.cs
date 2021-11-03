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
    public partial class FormAluno : Form
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V = new TurmaView();
        AlunoController Aluno_C = new AlunoController();
        public FormAluno(string nick)
        {
            InitializeComponent();
            textBoxUso.Text = nick;
        }

        private void notasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            dataGridView1.Visible = true;
            groupBox3.Visible = false;

            dataGridView1.DataSource=Aluno_C.VisualizarNota(textBoxUso.Text);
        }

        private void recuperaçãoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;

            comboBox1.ValueMember = "mat_id";
            comboBox1.DisplayMember = "mat_nome";

            comboBox1.DataSource = Aluno_C.ChamarMateria();
        }

        private void segundaChamadaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            dataGridView1.Visible = false;
            groupBox3.Visible = false;

            comboBox2.ValueMember = "mat_id";
            comboBox2.DisplayMember = "mat_nome";

            comboBox2.DataSource = Aluno_C.ChamarMateria();
        }

        private void avisosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;

            dataGridView1.DataSource = Aluno_C.ReceberAviso(textBoxUso.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja Sair do Portal de Notas?", "Log off", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Saindo do Portal");

                FormLogin newForm2 = new FormLogin();
                newForm2.Closed += (s, args) => this.Close();
                newForm2.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tem certeza de deseja encerrar o Portal?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void faltasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            dataGridView1.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            dataGridView1.DataSource = Aluno_C.VisualizarFaltas(textBoxUso.Text);
            textBox1.Text = Convert.ToString(Aluno_C.TotFaltas(textBoxUso.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aviso_V.Avi_recebeu = "Pedagogo(a)";
            Aviso_V.Avi_texto = "Recuperação de "+ comboBox1.Text;
            Aviso_V.Avi_mando = textBoxUso.Text;

            Aluno_C.MandarAviso(Aviso_V);

            MessageBox.Show("Solicitação enviada com sucesso!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Aviso_V.Avi_recebeu = "Pedagogo(a)";
            Aviso_V.Avi_texto = "Segunda chamada de " + comboBox1.Text;
            Aviso_V.Avi_mando = textBoxUso.Text;

            Aluno_C.MandarAviso(Aviso_V);

            MessageBox.Show("Solicitação enviada com sucesso!");
        }
    }
}

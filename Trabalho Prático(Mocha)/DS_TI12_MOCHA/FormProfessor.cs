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
    public partial class FormProfessor : Form
    {
        AvisosView Aviso_V = new AvisosView();
        TurmaView Turma_V = new TurmaView();
        ProfessorController Professor_C = new ProfessorController();
        Aluno_MateriaView AlMat_V = new Aluno_MateriaView();
        FaltaView Fal_V = new FaltaView();
        public FormProfessor(string nick)
        {
            InitializeComponent();
            textBoxUso.Text = nick;
        }

        private void avisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }
        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = true;

            dateTimePicker1.Visible = false;
            button2.Visible = true;
            button6.Visible = true;
            label1.Visible = true;
            comboBox1.Visible = true;

            comboBox1.ValueMember = "tur_id";
            comboBox1.DisplayMember = "tur_nome";

            comboBox1.DataSource = Professor_C.ChamarTurmas();
        }

        private void segundaChamadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
        }

        private void recuperaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox2.Visible = false;
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

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Tem certeza de deseja encerrar o Portal?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void mensagensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            dataGridView1.DataSource = Professor_C.ReceberAviso(textBoxUso.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button6.Visible = false;
            groupBox3.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            dataGridView1.DataSource = Professor_C.VisualizarTurma(textBoxUso.Text, comboBox1.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                AlMat_V.Alm_academico = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                AlMat_V.Alm_nota1 = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                AlMat_V.Alm_nota2 = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                AlMat_V.Alm_simulado = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);
                AlMat_V.Alm_trab1 = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value);
                AlMat_V.Alm_avalia = Convert.ToString(dataGridView1.Rows[i].Cells[5].Value);
                AlMat_V.Alm_turma = comboBox1.Text;
                AlMat_V.Alm_materia = Convert.ToString(textBoxUso.Text);

                Professor_C.UpdateNotasTurma(AlMat_V);
            }
            MessageBox.Show("Notas atualizadas!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == false && checkBox2.Checked == false)
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
            if (Aviso_V.Avi_recebeu != "Ninguém")
            {
                Aviso_V.Avi_texto = textBox1.Text;
                Aviso_V.Avi_mando = textBoxUso.Text;

                Professor_C.MandarAviso(Aviso_V);
                MessageBox.Show("Mensagem enviada com sucesso!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;
            button2.Visible = false;
            button6.Visible = false;
            label1.Visible = false;
            comboBox1.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            button1.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.DataSource = Professor_C.ListadeFaltas(comboBox1.Text);

            var col = new DataGridViewCheckBoxColumn();
            col.Name = "Falta";
            col.HeaderText = "Falta";
            col.FalseValue = "0";
            col.TrueValue = "1";

            col.CellTemplate.Value = false;
            col.CellTemplate.Style.NullValue = false;

            dataGridView1.Columns.Insert(1, col);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Fal_V.Fal_data = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                Fal_V.Fal_id = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);

                if (dataGridView1.Rows[i].Cells[1].Value == "1")
                {
                    Professor_C.InserirFaltas(Fal_V);
                }
            }
        }
    }
}

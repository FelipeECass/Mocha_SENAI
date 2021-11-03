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
    public partial class FormMatProfessor : Form
    {
        MatProfessorView MatProf_V = new MatProfessorView();
        MatProfessorController MatProf_C = new MatProfessorController();
        public FormMatProfessor()
        {
            InitializeComponent();

            comboBox1.ValueMember = "mat_id";
            comboBox1.DisplayMember = "mat_nome";

            comboBox1.DataSource = MatProf_C.ChamarMateria();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCadastro Fc = new FormCadastro();
            Fc.Closed += (s, args) => this.Close();
            Fc.Show();

            MatProf_V.Mat_nome = comboBox1.SelectedValue.ToString();

            MatProf_C.InserirNovaMateriaProfessor(MatProf_V);

        }
    }
}

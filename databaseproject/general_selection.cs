using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace databaseproject
{
    public partial class general_selection : Form
    {
        public general_selection()
        {
            InitializeComponent();
        }

        private void addInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student_Generral form = new Student_Generral();
            form.Show();
            
        }

        private void makeGoupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group G = new Group();
            G.Show();
            
        }

        private void addAdvisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General_Advisor g = new General_Advisor();
            g.Show();
           
        }

        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project p = new project();
            
            p.Show();
        }

        private void addEvaluationCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Evaluation E = new Evaluation();
            
            E.Show();
        }

        private void addProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProjectAdvisor p = new ProjectAdvisor();
            p.Show();
            this.Hide();
        }

        private void selectAdvisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Groupproject p = new Groupproject();
           
            p.Show();
          
        }

        private void groupEvaluationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Groupevaluation hh = new Groupevaluation();
            hh.Show();
            
        }

        private void general_selection_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            report1 r = new report1();
            r.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Report2 r = new Report2();
                r.Show();
        }
    }
}

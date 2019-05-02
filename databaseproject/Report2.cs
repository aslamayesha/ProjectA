using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Xml.Linq;
using CrystalDecisions.Shared;

namespace databaseproject
{
    public partial class Report2 : Form
    {
        public Report2()
        {
            InitializeComponent();
            ReportDocument rpt = new ReportDocument();
            SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True");
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter("select Student.RegistrationNo,Evaluation.Name,GroupEvaluation.ObtainedMarks,Project.Title from GroupStudent join Student on StudentId=GroupStudent.StudentId join GroupEvaluation on GroupEvaluation.GroupId=GroupStudent.GroupId join Evaluation on GroupEvaluation.EvaluationId=Evaluation.Id join GroupProject on GroupProject.GroupId=GroupEvaluation.GroupId join Project on GroupProject.ProjectId=ProjectId", con);
            DataSet2 dat = new DataSet2();
            DataTable T = new DataTable();
            ada.Fill(T);
            DataTable d_clone = T.Clone();
            d_clone.Columns[2].DataType = typeof(string);
            foreach (DataRow row in T.Rows)
            {
                d_clone.ImportRow(row);
                
            }
            dat.Tables[0].Merge(d_clone);
            rpt.Load(@"C:\Users\FINEC\Documents\Visual Studio 2015\Projects\databaseproject\databaseproject\CrystalReport2.rpt");
            rpt.SetDataSource(dat);
            crystalReportViewer1.ReportSource = rpt;



            con.Close();
        }

        private void Report2_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

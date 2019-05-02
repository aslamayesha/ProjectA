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
    public partial class report1 : Form
    {
        public report1(int i)
        {
            InitializeComponent();
            if(i==1)
            {
                ReportDocument rpt = new ReportDocument();
                SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True");
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select Project.Title,Person.FirstName,Student.RegistrationNo from Project join ProjectAdvisor  on Project.Id=ProjectAdvisor.ProjectId join Person on Person.Id=ProjectAdvisor.AdvisorId join GroupProject on GroupProject.ProjectId=ProjectAdvisor.ProjectId join GroupStudent on GroupStudent.GroupId=GroupProject.GroupId join Student on GroupStudent.StudentId=Student.Id", con);
                DataSet1 dat = new DataSet1();
                DataTable T = new DataTable();
                ada.Fill(T);
                //MessageBox.Show(dat.Tables[0].Rows.Count.ToString());
                dat.Tables[0].Merge(T);
                rpt.Load(@"C:\Users\FINEC\Documents\Visual Studio 2015\Projects\databaseproject\databaseproject\CrystalReport1.rpt");
                rpt.SetDataSource(dat);
                crystalReportViewer1.ReportSource = rpt;

                con.Close();
            }
          
        }

        public report1()
        {
           
            InitializeComponent();
            ReportDocument rpt = new ReportDocument();
            SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True");
            con.Open();
            SqlDataAdapter ada = new SqlDataAdapter("select Project.Title,Person.FirstName,Student.RegistrationNo from Project join ProjectAdvisor  on Project.Id=ProjectAdvisor.ProjectId join Person on Person.Id=ProjectAdvisor.AdvisorId join GroupProject on GroupProject.ProjectId=ProjectAdvisor.ProjectId join GroupStudent on GroupStudent.GroupId=GroupProject.GroupId join Student on GroupStudent.StudentId=Student.Id", con);
            DataSet1 dat = new DataSet1();
            DataTable T = new DataTable();
            ada.Fill(T);
            //MessageBox.Show(dat.Tables[0].Rows.Count.ToString());
            dat.Tables[0].Merge(T);
            MessageBox.Show(dat.Tables[0].Rows.Count.ToString());
            rpt.Load(@"C:\Users\FINEC\Documents\Visual Studio 2015\Projects\databaseproject\databaseproject\CrystalReport1.rpt");
            rpt.SetDataSource(dat);
            crystalReportViewer1.ReportSource = rpt;
            con.Close();


        }

        private void report1_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

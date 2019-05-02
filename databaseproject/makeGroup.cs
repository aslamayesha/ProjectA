using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace databaseproject
{
    public partial class makeGroup : Form
    
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int id=0;
        int insert = 0;
        int g;
        int s;
       
        public makeGroup(int i,int k,int gg,int ss)
        {
            id = i;
            insert = k;
            g = gg;
            s = ss;
            InitializeComponent();
            if (id == 1)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlDataAdapter sdaa = new SqlDataAdapter("Select Person.Id,Person.FirstName,Person.LastName,Person.Contact,Person.Email,Student.RegistrationNo from Person join Student on Person.id = Student.id", con);
                DataTable TT = new DataTable();
                sdaa.Fill(TT);
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Buttons";
                btn.Name = "ADD";
                btn.Text = "Add";
                btn.UseColumnTextForButtonValue = true;
                StudentDatagrid.Columns.Add(btn);
                StudentDatagrid.DataSource = TT;
            }
            else if (id == 2)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlDataAdapter sdaa = new SqlDataAdapter("Select Person.Id,Person.FirstName,Person.LastName,Person.Contact,Person.Email from Person join Advisor on Person.id = Advisor.Id", con);
                DataTable TT = new DataTable();
                sdaa.Fill(TT);
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Buttons";
                btn.Name = "ADD";
                btn.Text = "Add";
                btn.UseColumnTextForButtonValue = true;
                StudentDatagrid.Columns.Add(btn);
                StudentDatagrid.DataSource = TT;
            }
        }
        public makeGroup()
        {
            InitializeComponent();
            if (id == 1)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlDataAdapter sdaa = new SqlDataAdapter("Select Person.Id,Person.FirstName,Person.LastName,Person.Contact,Person.Email,Student.RegistrationNo from Person join Student on Person.id = Student.id", con);
                DataTable TT = new DataTable();
                sdaa.Fill(TT);
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Buttons";
                btn.Name = "ADD";
                btn.Text = "Add";
                btn.UseColumnTextForButtonValue = true;
                StudentDatagrid.Columns.Add(btn);
                StudentDatagrid.DataSource = TT;
            }
            else if(id==2)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlDataAdapter sdaa = new SqlDataAdapter("Select Person.Id,Person.FirstName,Person.LastName,Person.Contact,Person.Email from Person join Advisor on Person.id = Advisor.Id", con);
                DataTable TT = new DataTable();
                sdaa.Fill(TT);
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Buttons";
                btn.Name = "ADD";
                btn.Text = "Add";
                btn.UseColumnTextForButtonValue = true;
                StudentDatagrid.Columns.Add(btn);
                StudentDatagrid.DataSource = TT;
            }
        }
        

        private void makeGroup_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (id == 1)
                {
                    if (StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        Group gg = new Group(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString(),g,s);
                        gg.Show();
                        this.Hide();
                    }
                }
                else if (id == 2)
                {
                    if (StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        ProjectAdvisor g = new ProjectAdvisor(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString(),insert);
                        g.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}

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
    
    public partial class ProjectAdvisor : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert = 0;
        string id;
        int pid;
        int Ad;
        
        public ProjectAdvisor()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Project]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["Title"].ToString());

            }
            SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Advisor]", con);
            DataTable T1 = new DataTable();
            sda1.Fill(T1);
            foreach (DataRow row in T1.Rows)
            {
                txtAdvisorid.Items.Add(row["Id"].ToString());

            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "ADD";
            btn.Text = "Update";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
            newgrid();
        }
        public ProjectAdvisor(string k,int i)
        {
           
            InitializeComponent();
            id = k;
            insert = i;
            txtAdvisorid.Text = id;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Project]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["Title"].ToString());

            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "ADD";
            btn.Text = "Update";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
            newgrid();
        }

        private void ProjectAdvisor_Load(object sender, EventArgs e)
        {
                
        }
        
        public void newgrid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select ProjectAdvisor.AdvisorId,ProjectAdvisor.ProjectId,Lookup.Value from ProjectAdvisor join Lookup on ProjectAdvisor.AdvisorRole=Lookup.Id;", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            dataGridView1.DataSource = TT;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = 0;
            string Advisorrole="";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Project]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                if(row["Title"].ToString()==comboBox1.Text)
                {
                    id = Convert.ToInt32(row["Id"].ToString());
                }

            }
            SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Advisor]", con);
            DataTable T = new DataTable();
            sda1.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["Id"].ToString() == txtAdvisorid.Text)
                {
                    Advisorrole = row["Designation"].ToString();
                }

            }
            if(insert==0)
            {
                string q2 = "insert into [ProjectAdvisor] values('" + Convert.ToInt32(txtAdvisorid.Text) + "','" + id + "','" + Advisorrole + "','" + DateTime.Now + "')";
                SqlCommand cmd2 = new SqlCommand(q2, con);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Advisor added");
                newgrid();
            }
            else if(insert>0)
            {
                string role = "";
                SqlDataAdapter sda2 = new SqlDataAdapter("select * from [Advisor]", con);
                DataTable T1 = new DataTable();
                sda2.Fill(T1);
                foreach (DataRow row in T1.Rows)
                {
                    if ( Ad== Convert.ToInt32(row["Id"]))
                    {
                        role = row["Designation"].ToString();
                    }

                }


                string q3 = "update [ProjectAdvisor] SET ProjectAdvisor.AdvisorId='" + Convert.ToInt32(txtAdvisorid.Text)+ "',ProjectAdvisor.ProjectId='" + id + "' where ProjectAdvisor.ProjectId='"+pid+ "' And ProjectAdvisor.AdvisorId='" + Ad + "'";
                SqlCommand cmd3 = new SqlCommand(q3, con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Updated");
                newgrid();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtAdvisorid.Text = dataGridView1.Rows[e.RowIndex].Cells["AdvisorId"].FormattedValue.ToString();
                    insert = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AdvisorId"].FormattedValue.ToString());
                    Ad = insert;

                    SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Project]", con);
                    DataTable T = new DataTable();
                    sda1.Fill(T);
                    foreach (DataRow row in T.Rows)
                    {
                        if (row["Id"].ToString() == dataGridView1.Rows[e.RowIndex].Cells["ProjectId"].FormattedValue.ToString())
                        {
                            comboBox1.Text = row["Title"].ToString();
                        }

                    }
                   

                    pid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProjectId"].FormattedValue.ToString());
                }
            }
        }

        private void txtAdvisorid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

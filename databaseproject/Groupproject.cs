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
    public partial class Groupproject : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert = 0;
        string id;
        int pid;
        int Ad;
        public Groupproject()
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
            SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Group]", con);
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

        private void Groupproject_Load(object sender, EventArgs e)
        {

        }
        public bool valid()
        {
            bool correct = true;
            int p=0;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from GroupProject", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Project]", con);
            DataTable T = new DataTable();
            sda1.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (comboBox1.Text == row["Title"].ToString())
                {
                    p = Convert.ToInt32(row["Id"].ToString());
                   
                }

            }
            foreach (DataRow row in TT.Rows)
            {
                if (Convert.ToInt32(row["ProjectId"].ToString() )== p && row["GroupId"].ToString()==txtAdvisorid.Text)
                {
                    correct = false;
                }

            }


            return correct;
        }
        public void newgrid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from GroupProject", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            dataGridView1.DataSource = TT;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = 0;
          bool correct=  valid();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Project]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            if (correct == true)
            {
                foreach (DataRow row in TT.Rows)
                {
                    if (row["Title"].ToString() == comboBox1.Text)
                    {
                        id = Convert.ToInt32(row["Id"].ToString());
                    }

                }
                if (insert == 0)
                {
                    string q2 = "insert into [GroupProject] values('" + id + "','" + Convert.ToInt32(txtAdvisorid.Text) + "','" + DateTime.Now + "')";
                    SqlCommand cmd2 = new SqlCommand(q2, con);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Project added");
                    newgrid();
                }
                else if (insert > 0)
                {



                    string q3 = "update [GroupProject] SET GroupProject.ProjectId='" + id + "',GroupProject.GroupId='" + Convert.ToInt32(txtAdvisorid.Text) + "' where GroupProject.ProjectId='" + pid + "' And GroupProject.GroupId='" + Ad + "'";
                    SqlCommand cmd3 = new SqlCommand(q3, con);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    newgrid();
                }
            }
            else
            {
                MessageBox.Show("this project has been assigned to group");
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
                    txtAdvisorid.Text = dataGridView1.Rows[e.RowIndex].Cells["GroupId"].FormattedValue.ToString();
                    insert = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["GroupId"].FormattedValue.ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            general_selection g = new general_selection();
            this.Hide();
            g.Show();
        }
    }
}

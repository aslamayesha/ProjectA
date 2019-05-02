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
    public partial class Groupevaluation : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert = 0;
        string id;
        int pid;
        int Ad;
        int marks;
        public Groupevaluation()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Group]", con);
            DataTable T1 = new DataTable();
            sda1.Fill(T1);
            foreach (DataRow row in T1.Rows)
            {
                txtAdvisorid.Items.Add(row["Id"].ToString());

            }
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Evaluation]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["name"].ToString());

            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "ADD";
            btn.Text = "Update";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
           newgrid();
        }

        private void Groupevaluation_Load(object sender, EventArgs e)
        {

        }
        public void newgrid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from GroupEvaluation", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            dataGridView1.DataSource = TT;

        }
        public bool already()
        {
            bool correct = true;
            int id = 0;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from Evaluation", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            if (txtAdvisorid.Text != "" && txtmarks.Text != "" && comboBox1.Text != "")
            { 
             foreach (DataRow row in TT.Rows)
            {
                if (row["name"].ToString() == comboBox1.Text)
                {
                    id = Convert.ToInt32(row["Id"].ToString());
                        
                }

            }
            SqlDataAdapter sdaa1 = new SqlDataAdapter("Select * from GroupEvaluation", con);
            DataTable TT1 = new DataTable();
            sdaa1.Fill(TT1);
            foreach (DataRow row in TT1.Rows)
            {
                if (Convert.ToInt32(row["EvaluationId"].ToString()) == id && txtAdvisorid.Text == row["GroupID"].ToString())
                {
                        
                        correct = false;
                }

            }
        }
            else
            {
                MessageBox.Show("input can not null");
            }
            return correct;

        }
        public bool is_alphabet(string input)
        {
            bool is_alphabet = input.Any(c => char.IsLetter(c));
            return is_alphabet;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int id = 0;
            bool valid = is_alphabet(txtmarks.Text);
            bool correct = already();
            if(valid==true)
            {
                MessageBox.Show("invalid input");
            }
            if (txtmarks.Text != "" && txtAdvisorid.Text != "" && comboBox1.Text != "" && valid == false && correct == true) 
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Evaluation]", con);
                DataTable TT = new DataTable();
                sdaa.Fill(TT);

                foreach (DataRow row in TT.Rows)
                {
                    if (row["name"].ToString() == comboBox1.Text)
                    {
                        id = Convert.ToInt32(row["Id"].ToString());
                    }

                }
                if (insert == 0)
                {
                    string q2 = "insert into [GroupEvaluation] values('" + Convert.ToInt32(txtAdvisorid.Text) + "','" + id + "','" + Convert.ToInt32(txtmarks.Text) + "','" + DateTime.Now + "')";
                    SqlCommand cmd2 = new SqlCommand(q2, con);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Evaluation done");
                    newgrid();
                }

                else if (insert > 0)
                {



                    string q3 = "update [GroupEvaluation] SET GroupEvaluation.GroupId='" + Convert.ToInt32(txtAdvisorid.Text) + "',GroupEvaluation.EvaluationId='" + id + "' ,GroupEvaluation.ObtainedMarks='" + Convert.ToInt32(txtmarks.Text) + "' where GroupEvaluation.EvaluationId='" + pid + "' And GroupEvaluation.GroupId='" + Ad + "'";
                    SqlCommand cmd3 = new SqlCommand(q3, con);
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                    newgrid();
                }
            }
            else
            {
                MessageBox.Show("your input is not correct");
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

                    SqlDataAdapter sda1 = new SqlDataAdapter("select * from [Evaluation]", con);
                    DataTable T = new DataTable();
                    sda1.Fill(T);
                    foreach (DataRow row in T.Rows)
                    {
                        if (row["Id"].ToString() == dataGridView1.Rows[e.RowIndex].Cells["EvaluationId"].FormattedValue.ToString())
                        {
                            comboBox1.Text = row["name"].ToString();
                        }

                    }
                    marks = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ObtainedMarks"].FormattedValue.ToString());
                    txtmarks.Text = dataGridView1.Rows[e.RowIndex].Cells["ObtainedMarks"].FormattedValue.ToString();
                    pid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["EvaluationId"].FormattedValue.ToString());
                }
            }
        }
    }
}

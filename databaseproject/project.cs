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
namespace databaseproject
{
    public partial class project : Form
    {
        int insert = 0;
        string title;
        string description;
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        public project()
        {
            InitializeComponent();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "update";
            btn.Text = "UPDATE";
            btn.UseColumnTextForButtonValue = true;
            StudentDatagrid.Columns.Add(btn);
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = "Delete";
            btn1.Name = "Delete";
            btn1.Text = "Delete";
            btn1.UseColumnTextForButtonValue = true;
            StudentDatagrid.Columns.Add(btn1);
        }
        bool already_contains(string Title)
        {
            bool k = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Project", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["Title"].ToString() == Title && insert!=Convert.ToInt32(row["Id"]))
                {
                    MessageBox.Show("this project already exists");
                    return true;
                }
            }
            return k;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                bool k = already_contains(txttitle.Text);
                if (k == false)
                {
                    if (insert == 0)
                    {
                        if (txttitle.Text != "" && txttitle.Text.ToString().Length<=50)
                        {
                            string q = "insert into [Project] values('" + txtdescription.Text.ToString() + "','" + txttitle.Text.ToString() + "')";
                            SqlCommand cmd = new SqlCommand(q, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("a new row1 inserted");
                            grid();
                            txttitle.Text = "";
                            txtdescription.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("title can not be null");
                        }

                    }
                    else if (insert > 0)
                    {
                        if (txttitle.Text != "" && txttitle.Text.ToString().Length <= 50)
                        {
                            string q2 = "update Project SET Project.Description='" + txtdescription.Text.ToString() + "',Project.Title='" + txttitle.Text.ToString() + "' where Project.Id='" + insert + "' ";
                            SqlCommand cmd2 = new SqlCommand(q2, con);
                            cmd2.ExecuteNonQuery();
                            grid();
                            txttitle.Text = "";
                            txtdescription.Text = "";
                            insert = 0;
                        }
                        else
                        {
                            MessageBox.Show("incorrect value");
                        }
                    }
                    else
                    {
                        MessageBox.Show("incorrect values");
                    }
                }
            }

        }

        private void grid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                //from Person join Advisor on Person.id = Advisor.Id,con); Id
                //Select Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Advisor.Salary,Advisor.Designation
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Project.Id, Project.Description,Project.Title from Project", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                //2
                StudentDatagrid.DataSource = TT;
                StudentDatagrid.Columns[2].Visible = false;

            }
            else
            {
                MessageBox.Show("connecton is not open");

            }
        }

        private void project_Load(object sender, EventArgs e)
        {
            grid();
        }

        private void StudentDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                if (StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {

                    StudentDatagrid.CurrentRow.Selected = true;
                    title = StudentDatagrid.Rows[e.RowIndex].Cells["Title"].FormattedValue.ToString();
                    txttitle.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Title"].FormattedValue.ToString();
                    description = StudentDatagrid.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                    txtdescription.Text = description;
                    insert = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                }


            }
            else if (e.ColumnIndex == 1)
            {

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                int id= Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                string q2 = "Delete from Project where Project.Id='" + id + "'";
                SqlCommand cmd2 = new SqlCommand(q2, con);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("data has been deleted");
                grid();
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

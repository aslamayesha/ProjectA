using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace databaseproject
{
    public partial class Evaluation : Form
    {
        int insert = 0;
        string Name;
        int total_weightage;
        int marks;

        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        public Evaluation()
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
            SqlDataAdapter sda = new SqlDataAdapter("select * from Evaluation", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["Name"].ToString() == Title && insert != Convert.ToInt32(row["Id"]))
                {
                    MessageBox.Show("this evaluation already exists");
                    return true;
                }

            }
            return k;
        }
        private void grid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                //from Person join Advisor on Person.id = Advisor.Id,con); Id
                //Select Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Advisor.Salary,Advisor.Designation
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Evaluation.Id, Evaluation.Name,Evaluation.TotalMarks,Evaluation.TotalWeightage from Evaluation", con);
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
        public bool is_alphabet(string input)
        {
            bool is_alphabet = input.Any(c => char.IsLetter(c));
            return is_alphabet;
        }
        bool correct(string k, string l)
        {
            bool kk = is_alphabet(k);
            bool wei = is_alphabet(l);
            if (kk == true)
            {
                lblcontactno.Text = "can not alpha numeric";

                return false;
            }
             if (wei == true)
            {
                lblLastname.Text = "can not alpha numeric";

                return false;
            }
            int j = Convert.ToInt32(k);
                int m = Convert.ToInt32(l);
                if (j > 100 || m > 100 || j < 0 || m < 0)
                {
                    lblLastname.Text = "can not be grester thean 100 or less than 0";
                    lblcontactno.Text = "can not be grester thean 100 or less than 0";
                    return false;
                }
                

                return true; 
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                bool k = already_contains(txtname.Text);
                bool test = correct(txtmarks.Text.ToString(), txtweightage.Text.ToString());

                if (k == false)
                {
                    if (insert == 0)
                    {
                        if (txtname.Text != "" && txtweightage.Text != "" && txtmarks.Text != "" && test==true )
                        {
                            string q = "insert into [Evaluation] values('" + txtname.Text.ToString() + "','" + Convert.ToInt32(txtmarks.Text.ToString()) + "','" + Convert.ToInt32(txtweightage.Text.ToString()) + "')";
                            SqlCommand cmd = new SqlCommand(q, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("a new row1 inserted");
                            grid();
                            txtname.Text = "";
                            txtweightage.Text = "";
                            txtmarks.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("incorrect input");
                        }

                    }
                    else if(insert>0)
                    {
                      
                            if (txtname.Text != "" && txtweightage.Text!="" && txtmarks.Text != "" && test == true)
                            {
                                string q2 = "update Evaluation SET Evaluation.Name='" + txtname.Text.ToString() + "',Evaluation.TotalMarks='" + Convert.ToInt32(txtmarks.Text.ToString()) + "' ,Evaluation.TotalWeightage='" + Convert.ToInt32(txtmarks.Text.ToString()) + "' where Evaluation.Id='" + insert + "' ";
                                SqlCommand cmd2 = new SqlCommand(q2, con);
                                cmd2.ExecuteNonQuery();
                                grid();
                            txtname.Text = "";
                            txtweightage.Text = "";
                            txtmarks.Text = "";
                                insert = 0;
                            }
                            else
                            {
                                MessageBox.Show("incorrect value");
                            }
                        
                    }

                }
            }
        }

        private void Evaluation_Load(object sender, EventArgs e)
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
                    Name = StudentDatagrid.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                    txtname.Text = Name;
                    txtmarks.Text = StudentDatagrid.Rows[e.RowIndex].Cells["TotalMarks"].FormattedValue.ToString();
                    marks = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["TotalMarks"].FormattedValue.ToString());
                    txtweightage.Text = StudentDatagrid.Rows[e.RowIndex].Cells["TotalWeightage"].FormattedValue.ToString();
                    total_weightage = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["TotalWeightage"].FormattedValue.ToString());
                    insert = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                }


            }
            else if (e.ColumnIndex == 1)
            {

                SqlConnection con = new SqlConnection(conString);
                con.Open();
                int id = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                string q2 = "Delete from Evaluation where Evaluation.Id='" + id + "'";
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

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
    public partial class Group : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert = 0;
        string data;
        int gid=0;
        
        int sid=0;
        public Group()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Group]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["Id"].ToString());

            }
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "ADD";
            btn.Text = "Update";
            btn.UseColumnTextForButtonValue = true;
            StudentDatagrid.Columns.Add(btn);
            newgrid();
        }
        public Group(string k,int g,int s)
        {
            InitializeComponent();
            data = k;
            MessageBox.Show(g.ToString());
            gid = g;
            sid = s;
            comboBox1.Text = g.ToString();


            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Group]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["Id"].ToString());

            }
            txtstudentid.Text = data;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Buttons";
            btn.Name = "ADD";
            btn.Text = "Update";
            btn.UseColumnTextForButtonValue = true;
            StudentDatagrid.Columns.Add(btn);
            newgrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           /* SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from Student", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            if (txtstudent1.Text != "" && txtstudent2.Text != "" && txtstudent3.Text!=""  && txtstatus.Text != "" && txtdatetime.Text != "" && txtproject.Text!="")
            {

                
                    
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        int R1id=0;
                    int R2id = 0;
                    int R3id = 0;
                    int gid=0;
                    int pid = 0;
                    int status = 0;
                        string q1 = "insert into [Group] values('" + Convert.ToDateTime(txtdatetime.Text) + "')";
                        SqlCommand cmd1 = new SqlCommand(q1, con);
                        cmd1.ExecuteNonQuery();
                    SqlDataAdapter sdaa1 = new SqlDataAdapter("select * from Lookup", con);
                    DataTable TT1 = new DataTable();
                    sdaa1.Fill(TT1);
                    SqlDataAdapter sdaa2 = new SqlDataAdapter("select max(Id) as ma from [Group]", con);
                    DataTable TT2 = new DataTable();
                    sdaa2.Fill(TT2);
                    foreach (DataRow row in TT2.Rows)
                    {
                            gid = Convert.ToInt32(row["ma"]);
                        
                    }
                    foreach (DataRow row in TT1.Rows)
                    {
                        if (row["Value"].ToString() == txtstatus.Text)
                        {
                           status = Convert.ToInt32(row["Id"]);
                        }

                    }

                    if (txtstudent1.Text != "")
                    {
                        
                        foreach (DataRow row in TT.Rows)
                        {
                            if (row["RegistrationNo"].ToString() == txtstudent1.Text )
                            {
                                R1id = Convert.ToInt32(row["Id"]);
                            }
                        }
                        foreach (DataRow row in TT.Rows)
                        {
                            if (row["RegistrationNo"].ToString() == txtstudent2.Text)
                            {
                                R2id = Convert.ToInt32(row["Id"]);
                            }
                        }
                        foreach (DataRow row in TT.Rows)
                        {
                            if (row["RegistrationNo"].ToString() == txtstudent3.Text)
                            {
                                R3id = Convert.ToInt32(row["Id"]);
                            }
                        }
                        
                        string q2 = "insert into [GroupStudent] values('"+gid+"','"+R1id+"','" +status  + "','"+ Convert.ToDateTime(txtdatetime.Text) + "')";
                        SqlCommand cmd2 = new SqlCommand(q2, con);
                        cmd2.ExecuteNonQuery();
                        string q3 = "insert into [GroupStudent] values('" + gid + "','" + R2id + "','" + status + "','" + Convert.ToDateTime(txtdatetime.Text) + "')";
                        SqlCommand cmd3 = new SqlCommand(q3, con);
                        cmd3.ExecuteNonQuery();
                        string q4 = "insert into [GroupStudent] values('" + gid + "','" + R3id + "','" + status + "','" + Convert.ToDateTime(txtdatetime.Text) + "')";
                        SqlCommand cmd4 = new SqlCommand(q4, con);
                        cmd4.ExecuteNonQuery();
                        if (txtproject.Text != "")
                        {
                            SqlDataAdapter sdap = new SqlDataAdapter("select * from Lookup", con);
                            DataTable Tp = new DataTable();
                            sdap.Fill(Tp);
                            foreach (DataRow row in Tp.Rows)
                            {
                                if (row["Title"].ToString() == txtproject.Text)
                                {
                                   pid = Convert.ToInt32(row["Id"]);
                                }
                            }
                            string q5 = "insert into [GroupProject] values('" + pid + "','" + gid + "','" + Convert.ToDateTime(txtdatetime.Text) + "')";
                            SqlCommand cmd5 = new SqlCommand(q5, con);
                            cmd5.ExecuteNonQuery();

                        }
                            MessageBox.Show("Group has been made");

                    }
                    }
                
            }
            else
            {
                MessageBox.Show("invalid fields");
            }*/
        }

        private void txtstudent1_TextChanged(object sender, EventArgs e)
        {
                    }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string q1 = "insert into [Group] values('" + DateTime.Now + "')";
            SqlCommand cmd1 = new SqlCommand(q1, con);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter sdaa = new SqlDataAdapter("select * from [Group]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                comboBox1.Items.Add(row["Id"].ToString());

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 1;
           // gid = Convert.ToInt32(comboBox1.Text);
            makeGroup g = new makeGroup(i,insert,gid,sid);
            this.Hide();
            g.Show();
        }

        private void Group_Load(object sender, EventArgs e)
        {

        }
       public void newgrid()
        {

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from [GroupStudent]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            StudentDatagrid.DataSource = TT;
        }
        public bool valid()
        {
            bool correct = true;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlDataAdapter sdaa = new SqlDataAdapter("Select * from [GroupStudent]", con);
            DataTable TT = new DataTable();
            sdaa.Fill(TT);
            foreach (DataRow row in TT.Rows)
            {
                if (row["StudentId"].ToString() ==txtstudentid.Text && row["GroupId"].ToString()==comboBox1.Text)
                {
                    correct = false;
                }
            }
            return correct;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);
            con.Open();
            bool correct = valid();
            if (correct == true)
            {
                if (insert == 0)
                {
                    string q2 = "insert into [GroupStudent] values('" + Convert.ToInt32(comboBox1.Text) + "','" + Convert.ToInt32(txtstudentid.Text) + "','" + 3 + "','" + DateTime.Now + "')";
                    SqlCommand cmd2 = new SqlCommand(q2, con);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("student added");
                    newgrid();
                }
                else if (insert > 0)
                {
                    string q2 = "update [GroupStudent] SET GroupStudent.GroupId='" + Convert.ToInt32(comboBox1.Text) + "' ,GroupStudent.StudentId='" + Convert.ToInt32(txtstudentid.Text) + "' where GroupStudent.GroupId='" + gid + "' And GroupStudent.StudentId='" + sid + "'";
                    SqlCommand cmd2 = new SqlCommand(q2, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Updated");
                    string q3 = "update [GroupStudent] SET GroupStudent.Status='" + 4 + "' where GroupStudent.StudentId='" + Convert.ToInt32(txtstudentid.Text) + "' And GroupStudent.GroupId!='" + Convert.ToInt32(comboBox1.Text) + "'";
                    SqlCommand cmd3 = new SqlCommand(q3, con);
                    cmd3.ExecuteNonQuery();
                    newgrid();
                }
            }
            else
            {
                MessageBox.Show("Group Already exists");
            }
        }

        private void StudentDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                if (StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    comboBox1.Text = StudentDatagrid.Rows[e.RowIndex].Cells["GroupId"].FormattedValue.ToString();
                    insert = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["GroupId"].FormattedValue.ToString());
                    gid = insert;
                    MessageBox.Show(StudentDatagrid.Rows[e.RowIndex].Cells["GroupId"].FormattedValue.ToString());
                    txtstudentid.Text= StudentDatagrid.Rows[e.RowIndex].Cells["StudentId"].FormattedValue.ToString();
                    sid = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["StudentId"].FormattedValue.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            general_selection g = new general_selection();
            g.Show();
            this.Hide();
        }
    }
}

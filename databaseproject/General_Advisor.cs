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
    public partial class General_Advisor : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert = 0;
        public General_Advisor()
        {
            InitializeComponent();
            dropdown();
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
        public void dropdown()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                DataTable T = new DataTable();
                sda.Fill(T);
                foreach (DataRow row in T.Rows)
                {
                    if (row["Category"].ToString() == "DESIGNATION")
                    {
                        txtDesignation.Items.Add(row["Value"].ToString());
                    }
                }

            }
            else
            {
                MessageBox.Show("can not connect to database");
            }
        }

        private void General_Advisor_Load(object sender, EventArgs e)
        {
            grid();
        }
        private void grid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                //from Person join Advisor on Person.id = Advisor.Id,con); Id
                //Select Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Advisor.Salary,Advisor.Designation
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Person.Id, Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Advisor.Salary,Advisor.Designation from Person join Advisor on Person.Id = Advisor.Id", con);
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
        Regex regex = new Regex(@"^\d+$");
        public bool IsNumeric(string input)
         {
            bool isDigitPresent = input.Any(c => char.IsDigit(c));
            return isDigitPresent;

        }

        public bool is_alphabet(string input)
        {
            bool is_alphabet= input.Any(c => char.IsLetter(c));
            return is_alphabet;
        }
        public  bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item))
                { return true; }
            }

            return false;
        }
      public bool email_validation(string email)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (!rEMail.IsMatch(txtemail.Text))

            {

                MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;

            }
            else
            {
                return true;
            }
        }
        bool function_unique(string name,string lname,string contact,string email,string salary)
        {
           
            bool correct = true;
            bool correct_name = IsNumeric(name);
         bool valid_contact = is_alphabet(contact);
            bool cn = hasSpecialChar(name);
            bool cl = hasSpecialChar(lname);
            bool con = hasSpecialChar(contact);
            bool correct_sala = hasSpecialChar(salary);
            bool correct_email = email_validation(email);
            bool correct_sal = is_alphabet(salary);
            bool correct_lname= IsNumeric(lname);
            if (correct_name == true || cn == true || name.Length > 100)
            {
                lblName.Text = "input name should not contain numeric letters or special character or length is too long";
               
            }
            if (correct_lname == true || cl == true || lname.Length > 100)
            {
                lblLastname.Text = "input name should not contain numeric letters or special character or length is too long";

            }
            if (valid_contact == true || con == true || contact.Length > 20)
            {
                lblcontactno.Text = "input contact should not contain Alphabet or special character or length is too long";

            }
            if (correct_email == false )
            {
                lblemail.Text = "email format is not correct";

            }

            if (correct_sala == true || correct_sal == true )
            {
                lblsal.Text = "incorrect input field";

            }
            if (correct_email == false)
            {
                return false;

            }
            if (correct_name == true || cn == true || name.Length > 100)
            {
                return false;

            }
            if (correct_lname == true || cl == true)
            {
                return false;

            }

            if (valid_contact == true || con == true)
            {
       
                return false;
            }
            if (correct_sala == true || correct_sal == true)
            {
                return false;

            }
            //correct_lname == false
            if ( name.Length > 100)
            {
              
                return false;
            }
            if ( lname.Length > 100 )
            {
                
                return false;
            }
            if (contact.Length > 20)
            {
               
                return false;
            }
            if (salary.Length > 18 || Convert.ToInt32(salary)<0)
            {

                return false;
            }

            return correct;
        }
        bool already_contains(string email)
        {
            bool k = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
           
            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if ( row["Email"].ToString() == email)
                {
                    lblemail.Text = "already contains email";
                    return true;
                }
            }
            return k;
        }
        bool already_contains_update(string email)
        {

            bool k = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            
            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["Email"].ToString() == email && Convert.ToInt32(row["Id"]) != insert)
                {
                    lblemail.Text = "already contains email";
                    return true;
                }
            }
            return k;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            bool valid;
            valid = function_unique(txtfirstname.Text.ToString(), txtlastname.Text.ToString(), txtcontact.Text.ToString(), txtemail.Text.ToString(), txtsalary.Text.ToString());
            bool valid_email;
            int val = 0;
            int g = 2;
            if (txtgender.Text == "Male")
            {
                g = 1;
            }
            
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (insert == 0)
                {
                    
                    valid_email = already_contains(txtemail.Text);
                    if (valid == true && valid_email == false)
                    {
                        SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                        DataTable T = new DataTable();
                        sda.Fill(T);
                        foreach (DataRow row in T.Rows)
                        {
                            if (row["Value"].ToString() == txtDesignation.Text)
                            {
                                val = Convert.ToInt32(row["Id"].ToString());
                            }
                        }
                        if (txtgender.Text != "" && txtemail.Text!="" && txtDesignation.Text != "" && txtgender.Text!="")
                        {
                            string q = "insert into [Person] values('" + txtfirstname.Text.ToString() + "','" + txtlastname.Text.ToString() + "','" + txtcontact.Text.ToString() + "','" + txtemail.Text.ToString() + "','" + Convert.ToDateTime(txtdatetime.Text) + "','" + g + "')";
                            SqlCommand cmd = new SqlCommand(q, con);
                            cmd.ExecuteNonQuery();
                            txtemail.Text = "";
                            txtfirstname.Text = "";
                            txtemail.Text = "";
                            txtlastname.Text = "";
                            txtsalary.Text = "";
                            txtcontact.Text = "";
                        }
                        else
                        {
                            lblemail.Text = "this field is required";
                            lblName.Text = "this field is required";
                            
                            
                            
                        }
                       
                        MessageBox.Show("a new row1 inserted");
                        int id = 0;
                        SqlDataAdapter sdaa = new SqlDataAdapter("select * from Person", con);
                        DataTable TT = new DataTable();
                        sdaa.Fill(TT);
                        foreach (DataRow row in TT.Rows)
                        {
                            if (row["Firstname"].ToString() == txtfirstname.Text && row["Email"].ToString() == txtemail.Text)
                            {
                                id = Convert.ToInt32(row["Id"]);
                            }
                        }
                        if (txtDesignation.Text != "" && txtemail.Text!="")
                        {
                            string q1 = "insert into [Advisor] values('" + id + "','" + val + "','" + Convert.ToDecimal(txtsalary.Text.ToString()) + "')";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("a new row2 inserted");
                            grid();
                        }
                        else
                        {
                            lblDesignation.Text = "this field is required";
                           lblemail.Text = "this field is required";
                        }
                    }
                    else
                    {
                        MessageBox.Show("wrong input cannot insert");
                    }
                }
                else if (insert > 0)
                {
                    bool correct = already_contains_update(txtemail.Text);
                    int gen = 2;
                    int desig = 0;
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (valid == true && correct == false )
                        {
                            if (txtgender.Text == "Male")
                            {
                                gen = 1;
                            }
                            SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                            DataTable T = new DataTable();
                            sda.Fill(T);
                            foreach (DataRow row in T.Rows)
                            {
                                if (row["Value"].ToString() == txtDesignation.Text)
                                {
                                    desig = Convert.ToInt32(row["Id"]);


                                }
                            }
                            MessageBox.Show(email);
                            string q1 = "update Person SET Person.FirstName='" + txtfirstname.Text.ToString() + "',Person.LastName='" + txtlastname.Text.ToString() + "',Person.Contact='" + txtcontact.Text.ToString() + "',Person.Email='" + txtemail.Text.ToString() + "',Person.DateOfBirth='" + Convert.ToDateTime(txtdatetime.Text) + "',Person.Gender='" + gen + "' where Person.Id='" + pid + "' ";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            string q2 = "update Advisor SET Advisor.Designation='" + desig + "',Advisor.Salary='" + Convert.ToInt32(txtsalary.Text) + "' where Advisor.Id='" + pid + "' ";
                            SqlCommand cmd2 = new SqlCommand(q2, con);
                            cmd2.ExecuteNonQuery();
                            grid();
                            insert = 0;
                        }
                        else
                        {
                            MessageBox.Show("incorrect field");
                        }
                    }


                }


                else
                {
                    MessageBox.Show("can not connect to database");
                }

            }
        }
        int id;
        string fname;
        string Lname;
        string contact;
        string email;
        string dateofbirth;
        int gender = 0;
        int designation;
        decimal salary;
        int pid=0;

        private void StudentDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    StudentDatagrid.CurrentRow.Selected = true;
                    fname = StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString();
                    txtfirstname.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString();
                    Lname = StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString();
                    txtlastname.Text = StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString();
                    contact = StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString();
                    email = StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                    txtcontact.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString();
                    txtemail.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                    dateofbirth = StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString();
                    txtdatetime.Text = StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString();
                    designation = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Designation"].FormattedValue.ToString());
                    pid = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                    salary = Convert.ToDecimal(StudentDatagrid.Rows[e.RowIndex].Cells["Salary"].FormattedValue.ToString());
                    txtsalary.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Salary"].FormattedValue.ToString();
                    txtDesignation.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Salary"].FormattedValue.ToString();
                    SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                    DataTable T = new DataTable();
                    sda.Fill(T);
                    insert = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                    foreach (DataRow row in T.Rows)
                    {
                        if (row["Id"].ToString() == StudentDatagrid.Rows[e.RowIndex].Cells["Designation"].FormattedValue.ToString())
                        {
                            txtDesignation.Text = row["Value"].ToString();
                        }
                    }
                    if (StudentDatagrid.Rows[e.RowIndex].Cells["Gender"].FormattedValue.ToString() == "1")
                    {
                        gender = 1;
                        txtgender.Text = "Male";
                    }
                    else
                    {
                        gender = 2;

                        txtgender.Text = "Female";
                    }


                }

            }
            else if (e.ColumnIndex == 1)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                MessageBox.Show("abc");
                //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
                SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
                DataTable T = new DataTable();
                sda.Fill(T);

                String fname = txtfirstname.Text.ToString();

                int val = Convert.ToInt32(StudentDatagrid.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());


                string q2 = "Delete from Advisor where Advisor.Id='" + val + "'";
                SqlCommand cmd2 = new SqlCommand(q2, con);
                cmd2.ExecuteNonQuery();
                string q3 = "Delete from Person where Person.Id='" + val + "'";
                SqlCommand cmd3 = new SqlCommand(q3, con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("data has been deleted");
                grid();
            }
        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /* SqlConnection con = new SqlConnection(conString);
             con.Open();
             int gen = 2;
             int desig=0;
             if (con.State == System.Data.ConnectionState.Open)
             {
                 if (txtgender.Text == "Male")
                 {
                     gen = 1;
                 }
                 SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                 DataTable T = new DataTable();
                 sda.Fill(T);
                 foreach (DataRow row in T.Rows)
                 {
                     if (row["Value"].ToString() ==txtDesignation.Text)
                     {
                         desig = Convert.ToInt32(row["Id"]);


                     }
                 }
                 MessageBox.Show(email);
                 string q1 = "update Person SET Person.FirstName='" + txtfirstname.Text.ToString() + "',Person.LastName='" + txtlastname.Text.ToString() + "',Person.Contact='" + txtcontact.Text.ToString() + "',Person.Email='" + txtemail.Text.ToString() + "',Person.DateOfBirth='" + Convert.ToDateTime(txtdatetime.Text) + "',Person.Gender='" + gen + "' where Person.Id='"+pid+"' ";
                 SqlCommand cmd1 = new SqlCommand(q1, con);
                 cmd1.ExecuteNonQuery();
                 string q2 = "update Advisor SET Advisor.Designation='" + desig + "',Advisor.Salary='" + Convert.ToInt32 (txtsalary.Text) + "' where Advisor.Id='"+pid+"' ";
                 SqlCommand cmd2 = new SqlCommand(q2, con);
                 cmd2.ExecuteNonQuery();
                 grid();

             }
             else
             {
                 MessageBox.Show("connecton is not open");
             }
         }*/
        }

        private void lblLastname_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            general_selection g = new general_selection();
            g.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            bool valid;
            valid = function_unique(txtfirstname.Text.ToString(), txtlastname.Text.ToString(), txtcontact.Text.ToString(), txtemail.Text.ToString(), txtsalary.Text.ToString());
            bool valid_email;
            int val = 0;
            int g = 2;
            if (txtgender.Text == "Male")
            {
                g = 1;
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                if (insert == 0)
                {

                    valid_email = already_contains(txtemail.Text);
                    if (valid == true && valid_email == false)
                    {
                        SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                        DataTable T = new DataTable();
                        sda.Fill(T);
                        foreach (DataRow row in T.Rows)
                        {
                            if (row["Value"].ToString() == txtDesignation.Text)
                            {
                                val = Convert.ToInt32(row["Id"].ToString());
                            }
                        }
                        if (txtgender.Text != "" && txtemail.Text != "" && txtDesignation.Text != "" && txtgender.Text != "")
                        {
                            string q = "insert into [Person] values('" + txtfirstname.Text.ToString() + "','" + txtlastname.Text.ToString() + "','" + txtcontact.Text.ToString() + "','" + txtemail.Text.ToString() + "','" + Convert.ToDateTime(txtdatetime.Text) + "','" + g + "')";
                            SqlCommand cmd = new SqlCommand(q, con);
                            cmd.ExecuteNonQuery();
                            txtemail.Text = "";
                            txtfirstname.Text = "";
                            txtemail.Text = "";
                            txtlastname.Text = "";
                            txtsalary.Text = "";
                            txtcontact.Text = "";
                        }
                        else
                        {
                            lblemail.Text = "this field is required";
                            lblName.Text = "this field is required";



                        }

                        MessageBox.Show("a new row1 inserted");
                        int id = 0;
                        SqlDataAdapter sdaa = new SqlDataAdapter("select * from Person", con);
                        DataTable TT = new DataTable();
                        sdaa.Fill(TT);
                        foreach (DataRow row in TT.Rows)
                        {
                            if (row["Firstname"].ToString() == txtfirstname.Text && row["Email"].ToString() == txtemail.Text)
                            {
                                id = Convert.ToInt32(row["Id"]);
                            }
                        }
                        if (txtDesignation.Text != "" && txtemail.Text != "")
                        {
                            string q1 = "insert into [Advisor] values('" + id + "','" + val + "','" + Convert.ToDecimal(txtsalary.Text.ToString()) + "')";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("a new row2 inserted");
                            grid();
                        }
                        else
                        {
                            lblDesignation.Text = "this field is required";
                            lblemail.Text = "this field is required";
                        }
                    }
                    else
                    {
                        MessageBox.Show("wrong input cannot insert");
                    }
                }
                else if (insert > 0)
                {
                    bool correct = already_contains_update(txtemail.Text);
                    int gen = 2;
                    int desig = 0;
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        if (valid == true && correct == false)
                        {
                            if (txtgender.Text == "Male")
                            {
                                gen = 1;
                            }
                            SqlDataAdapter sda = new SqlDataAdapter("select * from Lookup", con);
                            DataTable T = new DataTable();
                            sda.Fill(T);
                            foreach (DataRow row in T.Rows)
                            {
                                if (row["Value"].ToString() == txtDesignation.Text)
                                {
                                    desig = Convert.ToInt32(row["Id"]);


                                }
                            }
                            MessageBox.Show(email);
                            string q1 = "update Person SET Person.FirstName='" + txtfirstname.Text.ToString() + "',Person.LastName='" + txtlastname.Text.ToString() + "',Person.Contact='" + txtcontact.Text.ToString() + "',Person.Email='" + txtemail.Text.ToString() + "',Person.DateOfBirth='" + Convert.ToDateTime(txtdatetime.Text) + "',Person.Gender='" + gen + "' where Person.Id='" + pid + "' ";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            string q2 = "update Advisor SET Advisor.Designation='" + desig + "',Advisor.Salary='" + Convert.ToInt32(txtsalary.Text) + "' where Advisor.Id='" + pid + "' ";
                            SqlCommand cmd2 = new SqlCommand(q2, con);
                            cmd2.ExecuteNonQuery();
                            grid();
                            insert = 0;
                        }
                        else
                        {
                            MessageBox.Show("incorrect field");
                        }
                    }


                }


                else
                {
                    MessageBox.Show("can not connect to database");
                }

            }

        }
    }
}

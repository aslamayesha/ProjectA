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

namespace databaseproject
{
    public partial class Student_Generral : Form
    {
        public string conString = "Data Source=FINE\\AYESHASLAM;Initial Catalog=ProjectA;Integrated Security=True";
        int insert=0;
        int id;
        string fname;
        string Lname;
        string contact;
        string email;
        string dateofbirth;
        int gender = 0;
        string R_g;
        public Student_Generral()
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
        public bool IsNumeric(string input)
        {
            bool isDigitPresent = input.Any(c => char.IsDigit(c));
            return isDigitPresent;

        }

        public bool is_alphabet(string input)
        {
            bool is_alphabet = input.Any(c => char.IsLetter(c));
            return is_alphabet;
        }
        public bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{};'<>,";
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
        bool function_unique(string name, string lname, string contact, string email, string rollno)
        {

            bool correct = true;
            bool correct_name = IsNumeric(name);
            bool valid_contact = is_alphabet(contact);
            bool cn = hasSpecialChar(name);
            bool cl = hasSpecialChar(lname);
            bool con = hasSpecialChar(contact);
            bool correct_sala = hasSpecialChar(rollno);
            bool correct_email = email_validation(email);
            
            bool correct_lname = IsNumeric(lname);
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
            if (correct_email == false)
            {
                lblemail.Text = "email format is not correct";

            }
            if (correct_sala == true )
            {
               lblRegistrationno.Text = "incorrect input field";

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
            if (correct_sala == true )
            {
                return false;

            }
            //correct_lname == false
            if (name.Length > 100)
            {

                return false;
            }
            if (lname.Length > 100)
            {

                return false;
            }
            if (contact.Length > 20)
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
            MessageBox.Show("abc");
            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["Email"].ToString() == email)
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
                if (row["Email"].ToString() == email && Convert.ToInt32( row["Id"])!=insert)
                {
                    lblemail.Text = "already contains email";
                    return true;
                }
            }
            return k;
        }
        bool already_contains_roll(string R_g)
        {

            bool k = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            
            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Student", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["RegistrationNo"].ToString() == R_g)
                {
                    lblRegistrationno.Text = "already contains registration no";
                    return true;
                }
            }
            return k;
        }
        bool already_contains_roll_update(string R_g)
        {

            bool k = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
           
            //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
            SqlDataAdapter sda = new SqlDataAdapter("select * from Student", con);
            DataTable T = new DataTable();
            sda.Fill(T);
            foreach (DataRow row in T.Rows)
            {
                if (row["RegistrationNo"].ToString() == R_g && Convert.ToInt32(row["Id"]) != insert)
                {
                    lblRegistrationno.Text = "already contains registration no";
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
            bool valid_email;
            valid = function_unique(txtfirstname.Text.ToString(), txtlastname.Text.ToString(), txtcontact.Text.ToString(), txtemail.Text.ToString(), txtregistrationno.Text.ToString());
            
            bool validrg = already_contains_roll(txtregistrationno.Text);
            if (insert == 0)
            {
                valid_email = already_contains(txtemail.Text);
                int g = 2;
                if (txtgender.Text == "Male")
                {
                    g = 1;
                }

                DateTime odate = Convert.ToDateTime(txtdatetime.Text);
                if (con.State == System.Data.ConnectionState.Open)
                {
                    
                    if (valid == true && valid_email == false && validrg==false)
                    {
                        if (txtgender.Text != "" && txtemail.Text != "" && txtregistrationno.Text != "" && txtgender.Text != "")
                        {
                            string q = "insert into [Person] values('" + txtfirstname.Text.ToString() + "','" + txtlastname.Text.ToString() + "','" + txtcontact.Text.ToString() + "','" + txtemail.Text.ToString() + "','" + Convert.ToDateTime(txtdatetime.Text) + "','" + g + "')";
                            SqlCommand cmd = new SqlCommand(q, con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("a new row1 inserted");

                            SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
                            DataTable T = new DataTable();
                            sda.Fill(T);

                            String fname = txtfirstname.Text.ToString();

                            int val = 0;
                            foreach (DataRow row in T.Rows)
                            {
                                if (row["Firstname"].ToString() == txtfirstname.Text && row["Email"].ToString() == txtemail.Text)
                                {
                                    val = Convert.ToInt32(row["Id"]);
                                }
                            }
                            string q1 = "insert into [Student] values('" + val + "','" + txtregistrationno.Text.ToString() + "')";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("a new row2 inserted");
                            SqlDataAdapter sda1 = new SqlDataAdapter("Select Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Student.RegistrationNo from Person join Student on Person.id = Student.id", con);
                            DataTable TT = new DataTable();
                            sda1.Fill(TT);
                            StudentDatagrid.DataSource = TT;
                            txtemail.Text = "";
                            txtfirstname.Text = "";
                            txtemail.Text = "";
                            txtlastname.Text = "";
                            txtregistrationno.Text = "";
                            txtcontact.Text = "";
                        }
                        else
                        {
                            if(txtemail.Text=="")
                            {
                                lblemail.Text = "this field is required";
                            }
                            if(txtregistrationno.Text=="")
                            {
                                lblRegistrationno.Text = "this filed is required";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("incorrect input");
                }
            }
            else if (insert > 0)
            {
                int gen = 2;
                if (con.State == System.Data.ConnectionState.Open)
             
                {
                    bool already_r_g = already_contains_roll_update(txtregistrationno.Text);
                   bool valid_update_email = already_contains_update(txtemail.Text);
                    if (valid == true && valid_update_email == false && already_r_g == false)
                    {
                        if (txtgender.Text != "" && txtemail.Text != "" && txtregistrationno.Text != "" && txtgender.Text != "")
                        {

                            if (txtgender.Text == "Male")
                            {
                                gen = 1;
                            }


                            string q1 = "update Person SET Person.FirstName='" + txtfirstname.Text.ToString() + "',Person.LastName='" + txtlastname.Text.ToString() + "',Person.Contact='" + txtcontact.Text.ToString() + "',Person.Email='" + txtemail.Text.ToString() + "',Person.DateOfBirth='" + Convert.ToDateTime(txtdatetime.Text) + "',Person.Gender='" + gen + "' where Person.Email='" + email + "' AND person.FirstName='" + fname + "' AND person.LastName='" + Lname + "' ";
                            SqlCommand cmd1 = new SqlCommand(q1, con);
                            cmd1.ExecuteNonQuery();
                            string q2 = "update Student SET Student.RegistrationNo='" + txtregistrationno.Text.ToString() + "' where Student.Id='" + insert + "' ";
                            SqlCommand cmd2 = new SqlCommand(q2, con);
                            cmd2.ExecuteNonQuery();
                            
                            grid();
                            
                            txtemail.Text = "";
                            txtfirstname.Text = "";
                            txtemail.Text = "";
                            txtlastname.Text = "";
                            txtregistrationno.Text = "";
                            txtcontact.Text = "";
                            insert = 0;
                        }
                        else
                        {
                            if (txtemail.Text == "")
                            {
                                lblemail.Text = "this field is required";
                            }
                            if (txtregistrationno.Text == "")
                            {
                                lblRegistrationno.Text = "this filed is required";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("incorrect field");
                    }
                }
                else
                {
                    MessageBox.Show("not open");

                }

            }
            else
            {
                MessageBox.Show("connecton is not open");
            }

            }
        

        private void Student_Generral_Load(object sender, EventArgs e)
        {
            grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            general_selection g = new general_selection();
            this.Hide();
            g.Show();
        }
        
        private void StudentDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex==0)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
               
                if(StudentDatagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value!=null)
                {
                    StudentDatagrid.CurrentRow.Selected = true;
                    fname= StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString();
                    txtfirstname.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString();
                    Lname= StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString();
                    txtlastname.Text = StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString();
                    contact= StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString();
                     email= StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                    txtcontact.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString();
                    txtemail.Text = StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                    dateofbirth= StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString();
                    txtdatetime.Text= StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString();
                    R_g= StudentDatagrid.Rows[e.RowIndex].Cells["RegistrationNo"].FormattedValue.ToString();
                    txtregistrationno.Text= StudentDatagrid.Rows[e.RowIndex].Cells["RegistrationNo"].FormattedValue.ToString();
                    if (StudentDatagrid.Rows[e.RowIndex].Cells["Gender"].FormattedValue.ToString()=="1")
                    {
                        gender = 1;
                        txtgender.Text = "Male";
                    }
                    else
                    {
                        gender = 2;
       
                        txtgender.Text = "Female";
                    }
                    SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
                    DataTable T = new DataTable();
                    sda.Fill(T);

                    

                    
                    foreach (DataRow row in T.Rows)
                    {
                        if (row["Firstname"].ToString() == StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() && row["Email"].ToString() == StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString())
                        {
                            insert = Convert.ToInt32(row["Id"]);
                        }
                    }
                    MessageBox.Show(insert.ToString());

                }
            }
            else if(e.ColumnIndex == 1)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                //string q1 = "select Person.Id from Person where Person.FirstName='" + StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() + "'AND Person.LastName='" + StudentDatagrid.Rows[e.RowIndex].Cells["LastName"].FormattedValue.ToString() + "' AND Person.Contact='" + StudentDatagrid.Rows[e.RowIndex].Cells["Contact"].FormattedValue.ToString() + "' AND Person.Email='" + StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString() + "'AND Person.DateOfBirth='" + Convert.ToDateTime(StudentDatagrid.Rows[e.RowIndex].Cells["DateOfBirth"].FormattedValue.ToString())+ "' ";
                SqlDataAdapter sda = new SqlDataAdapter("select * from Person", con);
                DataTable T = new DataTable();
                sda.Fill(T);

                String fname = txtfirstname.Text.ToString();

                int val = 0;
                foreach (DataRow row in T.Rows)
                {
                    if (row["Firstname"].ToString() == StudentDatagrid.Rows[e.RowIndex].Cells["Firstname"].FormattedValue.ToString() && row["Email"].ToString() == StudentDatagrid.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString())
                    {
                        id = Convert.ToInt32(row["Id"]);
                    }
                }
               
                string q2 = "Delete from Student where Student.Id='"+id+"'";
                SqlCommand cmd2 = new SqlCommand(q2, con);
                cmd2.ExecuteNonQuery();
                string q3 = "Delete from Person where Person.Id='" + id + "'";
                SqlCommand cmd3 = new SqlCommand(q3, con);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("data has been deleted");
                grid();
            }
        }
        private void grid()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select Person.FirstName,Person.LastName,Person.Contact,Person.Email,Person.DateOfBirth,Person.Gender,Student.RegistrationNo from Person join Student on Person.id = Student.id", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                StudentDatagrid.DataSource = TT;
            }
            else
            {
                MessageBox.Show("connecton is not open");

            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
           /* SqlConnection con = new SqlConnection(conString);
            con.Open();
            int gen = 2;
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (txtgender.Text == "Male")
                {
                    gen = 1;
                }

                MessageBox.Show(email);
                string q1 = "update Person SET Person.FirstName='" + txtfirstname.Text.ToString() + "',Person.LastName='" + txtlastname.Text.ToString() + "',Person.Contact='" + txtcontact.Text.ToString() + "',Person.Email='" + txtemail.Text.ToString() + "',Person.DateOfBirth='" + Convert.ToDateTime(txtdatetime.Text) + "',Person.Gender='" + gen + "' where Person.Email='" + email + "' AND person.FirstName='" + fname + "' AND person.LastName='" + Lname + "' ";
                SqlCommand cmd1 = new SqlCommand(q1, con);
                cmd1.ExecuteNonQuery();
                string q2 = "update Student SET Student.RegistrationNo='" + txtfirstname.Text.ToString() + "' where Student.RegistrationNo='"+R_g+"' ";
                SqlCommand cmd2 = new SqlCommand(q1, con);
                cmd2.ExecuteNonQuery();
                grid();
                
            }
            else
            {
                MessageBox.Show("connecton is not open");
            }*/

        }
    }
}

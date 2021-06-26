using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MoneyFlow
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
            tbpassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbusername.Text;
            string password = tbpassword.Text;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter(@"Select count(*) from LoginCredentials where Username = '" + username + "' and Password = '" + password + "'", con);
            DataTable dta = new DataTable();
            sda.Fill(dta);
            if (dta.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                btnExit m = new btnExit();
                m.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbpassword.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbusername.Text = "";
            tbpassword.Text = "";
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }
    }
}

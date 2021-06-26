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
    public partial class Credentials : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\paviliondv6\Documents\MoneyFlowDB.mdf;Integrated Security=True;Connect Timeout=30");
        public Credentials()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbOldPass.Text = "";
            tbNewUser.Text = "";
            tbNewPass.Text = "";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            con.Open();


            if (IfAdProductExists(con, tbOldPass.Text))
            {
                SqlCommand cmd = new SqlCommand("update LoginCredentials SET Username = '" + tbNewUser.Text + "',Password = '" + tbNewPass.Text + "'  WHERE Password = '" + tbOldPass.Text + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Login Information Altered.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                MessageBox.Show("Invalid Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();

            tbOldPass.Text = "";
            tbNewUser.Text = "";
            tbNewPass.Text = "";
        }

        private bool IfAdProductExists(SqlConnection con, string UserName)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select 1 from LoginCredentials WHERE Password = '" + UserName + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginPage l = new LoginPage();
            this.Hide();
            l.Show();
        }
    }
}

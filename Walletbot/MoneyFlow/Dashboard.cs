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
    public partial class btnExit : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\paviliondv6\Documents\MoneyFlowDB.mdf;Integrated Security=True;Connect Timeout=30");
        public btnExit()
        {
            InitializeComponent();
            refresh();
            recshow();

        }

        private void recshow()
        {
            con.Open();
            var ppquery = "";
            var pquery = "";
            var vnquery = "";

            ppquery = "select Amount from Report where Entities = 'Papi_Pet'";
            pquery = "select Amount from Report where Entities = 'Personal'";
            vnquery = "select Amount from Report where Entities = 'Vraman_Nepal'";

            SqlDataAdapter pp = new SqlDataAdapter(ppquery, con);
            SqlDataAdapter p = new SqlDataAdapter(pquery, con);
            SqlDataAdapter vn = new SqlDataAdapter(vnquery, con);

            DataTable d = new DataTable();
            pp.Fill(d);
            DataTable dt = new DataTable();
            p.Fill(dt);
            DataTable dta = new DataTable();
            vn.Fill(dta);

            lblAmtPapiPet.Text = d.Rows[0][0].ToString();
            lblAmtPersonal.Text = dt.Rows[0][0].ToString();
            lblAmtVramanNepal.Text = dta.Rows[0][0].ToString();

            con.Close();
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIncReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            IncomeReport ir = new IncomeReport();
            ir.Show();
        }

        private void btnExpReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExpenseReport er = new ExpenseReport();
            er.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginPage lp = new LoginPage();
            lp.Show();

        }

        private void btnClearInc_Click(object sender, EventArgs e)
        {
            cbIncSource.SelectedIndex = 0;
            tbIncAmt.Text = "";
            tbIncRemarks.Text = "";
        }

        private void btnExpClear_Click(object sender, EventArgs e)
        {
            tbExpAmt.Text = "";
            cbSpentOn.SelectedIndex = 0;
            cbExpSource.SelectedIndex = 0;
            tbExpRemarks.Text = "";
        }

        private void tbAmtPp_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            
            string source = cbIncSource.Text;
            string amount = tbIncAmt.Text;
            string remarks = tbIncRemarks.Text;
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            

            con.Open();
            var sqlquery = "";
            sqlquery = "insert into Income(Source,Amount,Remarks,Date) values ('" + source + "','" + amount + "','" + remarks + "','"+date+"')";

            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            
            MessageBox.Show("Data Entered Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            con.Close();
            adder();
            ResetRec();
        }

        private void refresh()
        {
            Int64 totpp = 0;
            Int64 totp = 0;
            Int64 totvn = 0;

            con.Open();
            var s = ""; var sq = ""; var sql = "";
            sql = "select Amount from Income where Source = 'Personal'";
            sq = "select Amount from Income where Source = 'Papi_Pet'";
            s = "select Amount from Income where Source = 'Vraman_Nepal'";

            SqlDataAdapter h = new SqlDataAdapter(sql, con);
            SqlDataAdapter ha = new SqlDataAdapter(sq, con);
            SqlDataAdapter hat = new SqlDataAdapter(s, con);

            DataTable dtas = new DataTable();
            DataTable dta = new DataTable();
            DataTable dt = new DataTable();
            
            h.Fill(dtas);
            ha.Fill(dta);
            hat.Fill(dt);

            Int32 i = dtas.Rows.Count;
            for (int k = 1; k<=i;k++)
            {
                totp = totp + Convert.ToInt64(dtas.Rows[k-1][0].ToString());
            }

            Int32 it = dta.Rows.Count;
            for (int k = 1;k<=it;k++)
            {
                totpp = totpp + Convert.ToInt64(dta.Rows[k-1][0].ToString());
            }

            Int32 itt = dt.Rows.Count;
            for (int k = 1; k <= itt; k++)
            {
                totvn = totvn + Convert.ToInt64(dt.Rows[k-1][0].ToString());
            }

        

            var e = ""; var et = ""; var eta = "";
            eta = "select Amount from Expense where Source = 'Personal'";
            et = "select Amount from Expense where Source = 'Papi_Pet'";
            e = "select Amount from Expense where Source = 'Vraman_Nepal'";

            SqlDataAdapter w = new SqlDataAdapter(eta, con);
            SqlDataAdapter wa = new SqlDataAdapter(et, con);
            SqlDataAdapter wat = new SqlDataAdapter(e, con);

            DataTable hdtas = new DataTable();
            DataTable hdta = new DataTable();
            DataTable hdt = new DataTable();

            w.Fill(hdtas);
            wa.Fill(hdta);
            wat.Fill(hdt);

            Int32 pi = hdtas.Rows.Count;
            for (int k = 1; k <= pi; k++)
            {
                totp = totp - Convert.ToInt64(hdtas.Rows[k-1][0].ToString());
            }

            Int32 pit = hdta.Rows.Count;
            for (int k = 1; k <= pit; k++)
            {
                totpp = totpp - Convert.ToInt64(hdta.Rows[k-1][0].ToString());
            }

            Int32 pitt = hdt.Rows.Count;
            for (int k = 1; k <= pitt; k++)
            {
                totvn = totvn - Convert.ToInt64(hdt.Rows[k-1][0].ToString());
            }

            var up = "";
            var updt = "";
            var updta = "";

            up = "update Report SET Amount = '" + totp.ToString() + "' WHERE Entities = 'Personal'";
            updt = "update Report SET Amount = '" + totpp.ToString() + "' WHERE Entities = 'Papi_Pet'";
            updta = "update Report SET Amount = '" + totvn.ToString() + "' WHERE Entities = 'Vraman_Nepal'";

            SqlCommand cmd = new SqlCommand(up, con);
            SqlCommand cm = new SqlCommand(updt, con);
            SqlCommand c = new SqlCommand(updta, con);

            cmd.ExecuteNonQuery();
            cm.ExecuteNonQuery();
            c.ExecuteNonQuery();

            con.Close();
        }
        private void adder()
        {
            string so = cbIncSource.Text;
            Int64 amt = Convert.ToInt64(tbIncAmt.Text);
            con.Open();
            var sql = "";
            sql = "select Amount from Report where Entities = '" + so + "'";

            SqlDataAdapter s = new SqlDataAdapter(sql, con);
            DataTable dtas = new DataTable();
            s.Fill(dtas);

            Int64 am = Convert.ToInt64(dtas.Rows[0][0].ToString());
            Int64 amount = am + amt;

            string finalamt = amount.ToString();

            var sqlquery = "";
            sqlquery = "update Report SET Amount = '"+finalamt+"' where Entities = '"+so+"'";

            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();

            con.Close();

            recshow();
        }

        private void tbIncAmt_Enter(object sender, EventArgs e)
        {
            if (tbIncAmt.Text == "0")
                tbIncAmt.Text = "";
            tbIncAmt.ForeColor = Color.Black;
        }

        private void tbExpAmt_Enter(object sender, EventArgs e)
        {
            if (tbExpAmt.Text == "0")
                tbExpAmt.Text = "";
            tbExpAmt.ForeColor = Color.Black;
        }

        private void ResetRec()
        {
            cbIncSource.SelectedIndex = 0;
            tbIncAmt.Text = "0";
            tbIncAmt.ForeColor = Color.Gray;
            tbIncRemarks.Text = "";
        }

        private void btnExpConfirm_Click(object sender, EventArgs e)
        {
            string amount = tbExpAmt.Text;
            string spenton = cbSpentOn.Text;
            string source = cbExpSource.Text;
            string remark = tbExpRemarks.Text;
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            con.Open();
            var sqlquery = "";
            sqlquery = "insert into Expense(Amount,Spent_on,Source,Remarks,Date) values ('" + amount + "','" + spenton + "','" + source + "','"+remark+"','"+date+"')";

            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();

            

            con.Close();
            subtracter();
            resetexp();

        }

        private void subtracter()
        {
            string so = cbExpSource.Text;
            float amt = Convert.ToInt64(tbExpAmt.Text);
            con.Open();
            var sql = "";
            sql = "select Amount from Report where Entities = '" + so + "'";

            SqlDataAdapter s = new SqlDataAdapter(sql, con);
            DataTable dtas = new DataTable();
            s.Fill(dtas);

            float am = Convert.ToInt64(dtas.Rows[0][0].ToString());
            float amount = am - amt;
            string finalamt = "";
            if (amount < 0)
            {
                MessageBox.Show("Insufficient Balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            else if (amount >= 0)
            {
                finalamt = amount.ToString();
                var sqlquery = "";
                sqlquery = "update Report SET Amount = '" + finalamt + "' where Entities = '" + so + "'";

                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Recorded!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();
                recshow();
            }

            con.Close();
            

            
        }
        private void resetexp()
        {
            tbExpAmt.Text = "0";
            tbExpAmt.ForeColor = Color.Gray;
            cbSpentOn.SelectedIndex = 0;
            cbExpSource.SelectedIndex = 0;
            tbExpRemarks.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Credentials cs = new Credentials();
            this.Hide();
            cs.Show();
        }
    }
}

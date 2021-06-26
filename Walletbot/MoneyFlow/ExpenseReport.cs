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
    public partial class ExpenseReport : Form
    {
        string ri = "";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\paviliondv6\Documents\MoneyFlowDB.mdf;Integrated Security=True;Connect Timeout=30");
        public ExpenseReport()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            btnExit ex = new btnExit();
            ex.Show();

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from Expense where Date between '" + dtpFrom.Value.Date.ToString() + "' and '" + dtpTo.Value.Date.ToString() + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dgvExpenseReport.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvExpenseReport.Rows.Add();
                dgvExpenseReport.Rows[n].Cells[0].Value = item["S_N"].ToString();
                dgvExpenseReport.Rows[n].Cells[1].Value = item["Amount"].ToString();
                dgvExpenseReport.Rows[n].Cells[2].Value = item["Spent_on"].ToString();
                dgvExpenseReport.Rows[n].Cells[3].Value = item["Source"].ToString();
                dgvExpenseReport.Rows[n].Cells[4].Value = item["Remarks"].ToString();
                dgvExpenseReport.Rows[n].Cells[5].Value = item["Date"].ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblid.Text = "";
            tbedtDestination.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;
        }

        private void dgvIncomeReport_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblid.Text = dgvExpenseReport.SelectedRows[0].Cells[0].Value.ToString();
            tbeditDate.Text = dgvExpenseReport.SelectedRows[0].Cells[5].Value.ToString();
            cbeditSource.Text = dgvExpenseReport.SelectedRows[0].Cells[3].Value.ToString();
            tbedtDestination.Text = dgvExpenseReport.SelectedRows[0].Cells[2].Value.ToString();
            tbEditAmt.Text = dgvExpenseReport.SelectedRows[0].Cells[1].Value.ToString();
            tbEditRemarks.Text = dgvExpenseReport.SelectedRows[0].Cells[4].Value.ToString();
            int rowindex = dgvExpenseReport.CurrentCell.RowIndex;
            ri = rowindex.ToString();
        }

        private void dgvExpenseReport_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblid.Text = dgvExpenseReport.SelectedRows[0].Cells[0].Value.ToString();
            tbeditDate.Text = dgvExpenseReport.SelectedRows[0].Cells[5].Value.ToString();
            cbeditSource.Text = dgvExpenseReport.SelectedRows[0].Cells[3].Value.ToString();
            tbedtDestination.Text = dgvExpenseReport.SelectedRows[0].Cells[2].Value.ToString();
            tbEditAmt.Text = dgvExpenseReport.SelectedRows[0].Cells[1].Value.ToString();
            tbEditRemarks.Text = dgvExpenseReport.SelectedRows[0].Cells[4].Value.ToString();
            int rowindex = dgvExpenseReport.CurrentCell.RowIndex;
            ri = rowindex.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();

            string id = lblid.Text;
            string edtdate = tbeditDate.Text;
            string edtsource = cbeditSource.Text;
            string destination = tbedtDestination.Text;
            string edtamount = tbEditAmt.Text;
            string edtremarks = tbEditRemarks.Text;



            int i = Convert.ToInt32(ri);

            dgvExpenseReport.Rows[i].Cells[0].Value = id;
            dgvExpenseReport.Rows[i].Cells[5].Value = edtdate;
            dgvExpenseReport.Rows[i].Cells[3].Value = edtsource;
            dgvExpenseReport.Rows[i].Cells[2].Value = destination;
            dgvExpenseReport.Rows[i].Cells[1].Value = edtamount;
            dgvExpenseReport.Rows[i].Cells[4].Value = edtremarks;

            var sqlquery = "";
            sqlquery = "update Expense SET Amount = '" + edtamount + "',Spent_on = '" + destination + "',Source = '" + edtsource + "',Remarks = '" + edtremarks + "',Date = '" + edtdate + "' WHERE S_N = '" + id + "'";

            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();

            lblid.Text = "";
            tbedtDestination.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;

            con.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvExpenseReport.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dgvExpenseReport.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dgvExpenseReport.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dgvExpenseReport.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvExpenseReport.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dgvExpenseReport.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }

        private void btnedtDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            string id = lblid.Text;
            
            var sq = "";
            sq = "delete FROM Expense WHERE S_N='" + id + "'";

            SqlCommand cmd = new SqlCommand(sq, con);
            cmd.ExecuteNonQuery();

            con.Close();

            lblid.Text = "";
            tbedtDestination.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;
        }
    }
}

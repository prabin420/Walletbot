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

    public partial class IncomeReport : Form
    {
        string ri = "";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30");
        public IncomeReport()
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
            
            SqlDataAdapter sda = new SqlDataAdapter("select * from Income where Date between '" + dtpFrom.Value.Date.ToString() + "' and '" + dtpTo.Value.Date.ToString() + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dgvIncomeReport.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dgvIncomeReport.Rows.Add();
                dgvIncomeReport.Rows[n].Cells[0].Value = item["S_N"].ToString();
                dgvIncomeReport.Rows[n].Cells[1].Value = item["Date"].ToString();
                dgvIncomeReport.Rows[n].Cells[2].Value = item["Source"].ToString();
                dgvIncomeReport.Rows[n].Cells[3].Value = item["Amount"].ToString();
                dgvIncomeReport.Rows[n].Cells[4].Value = item["Remarks"].ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblid.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;
        }

        private void dgvIncomeReport_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lblid.Text = dgvIncomeReport.SelectedRows[0].Cells[0].Value.ToString();
            tbeditDate.Text = dgvIncomeReport.SelectedRows[0].Cells[1].Value.ToString();
            cbeditSource.Text = dgvIncomeReport.SelectedRows[0].Cells[2].Value.ToString();
            tbEditAmt.Text = dgvIncomeReport.SelectedRows[0].Cells[3].Value.ToString();
            tbEditRemarks.Text = dgvIncomeReport.SelectedRows[0].Cells[4].Value.ToString();
            int rowindex = dgvIncomeReport.CurrentCell.RowIndex;
            ri = rowindex.ToString();
            
        }

        private void dgvIncomeReport_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblid.Text = dgvIncomeReport.SelectedRows[0].Cells[0].Value.ToString();
            tbeditDate.Text = dgvIncomeReport.SelectedRows[0].Cells[1].Value.ToString();
            cbeditSource.Text = dgvIncomeReport.SelectedRows[0].Cells[2].Value.ToString();
            tbEditAmt.Text = dgvIncomeReport.SelectedRows[0].Cells[3].Value.ToString();
            tbEditRemarks.Text = dgvIncomeReport.SelectedRows[0].Cells[4].Value.ToString();
            int rowindex = dgvIncomeReport.CurrentCell.RowIndex;
            ri = rowindex.ToString();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();

            string id = lblid.Text;
            string edtdate = tbeditDate.Text;
            string edtsource = cbeditSource.Text;
            string edtamount = tbEditAmt.Text;
            string edtremarks = tbEditRemarks.Text;



            int i = Convert.ToInt32(ri);

            dgvIncomeReport.Rows[i].Cells[0].Value = id;
            dgvIncomeReport.Rows[i].Cells[1].Value = edtdate;
            dgvIncomeReport.Rows[i].Cells[2].Value = edtsource;
            dgvIncomeReport.Rows[i].Cells[3].Value = edtamount;
            dgvIncomeReport.Rows[i].Cells[4].Value = edtremarks;

            var sqlquery = "";
            sqlquery = "update Income SET Source = '"+edtsource+"',Amount = '"+edtamount+"',Remarks = '"+edtremarks+"',Date = '"+edtdate+"' WHERE S_N = '"+id+"'";

            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();

            lblid.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;

            con.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvIncomeReport.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dgvIncomeReport.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dgvIncomeReport.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dgvIncomeReport.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvIncomeReport.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dgvIncomeReport.Rows[i].Cells[j].Value.ToString();
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
            sq = "delete FROM Income WHERE S_N='" + id + "'";

            SqlCommand cmd = new SqlCommand(sq, con);
            cmd.ExecuteNonQuery();

            con.Close();

            lblid.Text = "";
            tbeditDate.Text = "";
            tbEditAmt.Text = "";
            tbEditRemarks.Text = "";
            cbeditSource.SelectedIndex = 0;
        }
    }
}

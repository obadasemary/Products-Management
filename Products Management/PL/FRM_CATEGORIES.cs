using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Products_Management.PL
{
    public partial class FRM_CATEGORIES : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=.\SQLEXPRESS; Database=Product_DB; Integrated Security=true");
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        BindingManagerBase bmb;
        SqlCommandBuilder cmdb;
        public FRM_CATEGORIES()
        {
            InitializeComponent();
            da = new SqlDataAdapter("SELECT ID_CAT as 'رقم',DESCRIPTION_CAT as 'الصنف' FROM CATEGORIES", sqlcon);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            txtID.DataBindings.Add("text", dt, "رقم");
            txtDes.DataBindings.Add("text", dt, "الصنف");
            bmb = this.BindingContext[dt];
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bmb.Position = 0;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bmb.Position = bmb.Count;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btbPrevious_Click(object sender, EventArgs e)
        {
            bmb.Position -= 1;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bmb.Position += 1;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bmb.AddNew();
            btnNew.Enabled = false;
            btnAdd.Enabled = true;
            int id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]) + 1;
            txtID.Text = id.ToString();
            txtDes.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
            btnAdd.Enabled = false;
            btnNew.Enabled = true;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bmb.RemoveAt(bmb.Position);
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Delete Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Edited Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;            
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            RPT.RPT_ALL_CAT rpt = new RPT.RPT_ALL_CAT();
            RPT.FRM_RPT_PRODUCT frm = new RPT.FRM_RPT_PRODUCT();
            rpt.Refresh();
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.Show();
        }
    }
}

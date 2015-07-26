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
    public partial class FRM_MAIN : Form
    {
        //Single Imstance
        private static FRM_MAIN frm;
        SqlConnection sqlconnection = new SqlConnection(@"Server=.\OBADA; Database=Product_DB; Integrated Security=true");
        SqlConnection Sqlconnection = new SqlConnection(@"Server=.\OBADA; Database=Master; Integrated Security=true");
        SqlCommand Cmd;
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }

        public static FRM_MAIN getMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new FRM_MAIN();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }
        public FRM_MAIN()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;            
            this.المنتجاتToolStripMenuItem.Enabled = false;
            this.العملاءToolStripMenuItem.Enabled = false;
            this.المستخدمينToolStripMenuItem.Enabled = false;
            this.إنشاءنسخةاحتياطيةToolStripMenuItem.Enabled = false;
            this.استعادةنسخةمحفوظةToolStripMenuItem.Enabled = false;
        }

        private void تسجيلالدخولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_LOGIN frm = new FRM_LOGIN();
            frm.ShowDialog();
        }

        private void ملفToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void إضافةمنتججديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
            frm.ShowDialog();
        }

        private void إدارةالمنتجاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_PRODUCTS frm = new FRM_PRODUCTS();
            frm.ShowDialog();
        }

        private void إنشاءنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Backup Files (*.Bak) |*.bak";
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                Cmd = new SqlCommand("Backup Database Product_DB To Disk ='" + SFD.FileName + "'", sqlconnection);
                sqlconnection.Open();
                Cmd.ExecuteNonQuery();
                sqlconnection.Close();
                MessageBox.Show("Backup Completed ", "Backup Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void استعادةنسخةمحفوظةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Backup Files (*.Bak) |*.bak";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                Cmd = new SqlCommand("ALTER DATABASE Product_DB SET OFFLINE WITH ROLLBACK IMMEDIATE; RESTORE DATABASE Product_DB From Disk ='" + OFD.FileName + "' WITH REPLACE", Sqlconnection);
                Sqlconnection.Open();
                Cmd.ExecuteNonQuery();
                Sqlconnection.Close();
                MessageBox.Show("Restore Completed ", "Restore Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

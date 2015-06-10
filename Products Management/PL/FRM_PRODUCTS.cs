using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Products_Management.PL
{
    public partial class FRM_PRODUCTS : Form
    {
        BL.CLS_PRODUCT prd = new BL.CLS_PRODUCT();
        public FRM_PRODUCTS()
        {
            InitializeComponent();
            this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
        }
    }
}

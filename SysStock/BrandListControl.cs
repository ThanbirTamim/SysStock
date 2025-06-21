using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysStock
{
    public partial class BrandListControl : UserControl
    {
        private MainForm mainForm;
        public BrandListControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void BrandListControl_Load(object sender, EventArgs e)
        {

        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            mainForm.LoadControl(new BrandCreateControl());
        }
    }
}

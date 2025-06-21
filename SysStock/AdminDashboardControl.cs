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
    public partial class AdminDashboardControl : UserControl
    {
        private MainForm mainForm;
        public AdminDashboardControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AdminDashboardControl_Load(object sender, EventArgs e)
        {
            lblHero.Text = "Welcome to SysStock, " + mainForm.CurrentUser + "!";
        }
    }
}

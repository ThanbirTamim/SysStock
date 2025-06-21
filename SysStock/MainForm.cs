using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysStock.Utility.Models;

namespace SysStock
{
    public partial class MainForm : Form
    {
        private User User;
        public string CurrentUser { get; internal set; }

        public MainForm(string username, User user)
        {
            InitializeComponent();
            CurrentUser = username;
            this.User = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblHeader.Text = "Welcome, " + CurrentUser + "!";
            LoadControl(new AdminDashboardControl(this));
        }




        public void LoadControl(UserControl control)
        {
            panelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Add(control);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            LoadControl(new UserListControl(this));
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            LoadControl(new ProductListControl(this, User.Role));
        }

        private void btnBrands_Click(object sender, EventArgs e)
        {
            LoadControl(new BrandListControl(this));
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            LoadControl(new CategoryListControl(this));
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadControl(new AdminDashboardControl(this));
        }

    }
}

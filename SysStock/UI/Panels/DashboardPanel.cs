using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysStock.Utility;
using SysStock.Utility.DataAccess;

namespace SysStock.UI.Panels
{
    public partial class DashboardPanel : UserControl
    {
        private MainPanel mainForm;
        public DashboardPanel(MainPanel mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (string.IsNullOrEmpty(txtCurrentPassword.Text) ||
                string.IsNullOrEmpty(txtNewPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.Text = "New passwords do not match.";
                return;
            }

            try
            {
                using (var userDal = new UserDAL())
                {
                    if (userDal.ChangePassword(CurrentUserSession.UserId, txtCurrentPassword.Text, txtNewPassword.Text))
                    {
                        lblMessage.Text = "Password changed successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        txtCurrentPassword.Clear();
                        txtNewPassword.Clear();
                        txtConfirmPassword.Clear();
                    }
                    else
                    {
                        lblMessage.Text = "Current password is incorrect.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

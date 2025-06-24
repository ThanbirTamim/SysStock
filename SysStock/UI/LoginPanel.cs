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

namespace SysStock.UI
{
    public partial class LoginPanel : Form
    {
        public string Username => txtUsername.Text.Trim();
        public string Password => txtPassword.Text;

        public LoginPanel()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    lblMessage.Text = "Please enter both username and password.";
                    return;
                }

                UserDAL userDAL = new UserDAL();
                var user = userDAL.Authenticate(username, password);
                if (user != null)
                {
                    CurrentUserSession.SetCurrentUser(user);


                    MessageBox.Show($"Welcome {user.FullName} ({user.Role})", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MainPanel main = new MainPanel(user.FullName, user);
                    this.Hide();
                    main.Show();
                }
                else
                {
                    lblMessage.Text = "Invalid credentials or user is inactive.";
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Database error: " + ex.Message;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
